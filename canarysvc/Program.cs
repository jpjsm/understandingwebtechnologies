using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.DataContracts;
using System.Linq;

namespace canarysvc
{
    class Program
    {
        internal static readonly ILogger _logger;
        internal static Dictionary<string, Runner> runners = new Dictionary<string, Runner>();
        //internal static IServiceCollection services;
        //internal static IServiceProvider serviceProvider;

        public static void Main(string[] args)
        {
            int retries = 3;
            bool loaded = false;
            List<Canary> canaries = null;
            TimeSpan elapsed;
            // Metrics setup ==> Microsoft.ApplicationInsights

            // Create the DI container.
            IServiceCollection services = new ServiceCollection();
            services.AddApplicationInsightsTelemetryWorkerService("18548be7-b8c9-422c-a96a-4902073b92ef");

            // Build ServiceProvider.
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Obtain TelemetryClient instance from DI, for additional manual tracking or to flush.
            var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();


            using (telemetryClient.StartOperation<RequestTelemetry>("ChangeManagerCanarySvc"))
            {

                while (true)
                {
                    DateTime timer = DateTime.UtcNow;
                    Exception ex = null;
                    // Load canaries
                    retries = 3;
                    loaded = false;

                    do
                    {
                        try
                        {
                            canaries = JsonConvert.DeserializeObject<List<Canary>>(File.ReadAllText(@"canaries.json"));
                            loaded = true;
                        }
                        catch (Exception x)
                        {
                            ex = x;
                            retries--;
                        }
                    }
                    while (!loaded && retries > 0);

                    if (!loaded)
                    {
                        ExceptionTelemetry ext = new ExceptionTelemetry(ex)
                        {
                            Message = "Canary svc couldn't load canary definitions 'canaries.json'",
                            SeverityLevel = SeverityLevel.Critical,
                            Timestamp = DateTime.UtcNow
                        };

                        telemetryClient.TrackException(ext);
                        //throw new ApplicationException("Canary svc couldn't load canary definitions 'canaries.json'");
                        Thread.Sleep(5000);
                        telemetryClient.Flush();
                        Console.WriteLine(ext.Message);
                        continue;
                    }

                    // Execute each canary independently
                    foreach (Canary canary in canaries)
                    {
                        ParameterizedThreadStart r = null;

                        switch (canary.Kind.ToLowerInvariant())
                        {
                            case "http":
                                r = HttpCanaryRun;
                                break;

                            case "sqlcnx":
                                r = SqlCnxCanaryRun;
                                break;

                            case "sqlstmt":
                                r = SqlStmtCanaryRun;
                                break;

                            default:
                                break;
                        }

                        if (!runners.ContainsKey(canary.CanaryName))
                        {
                            runners.Add(canary.CanaryName, new Runner() { Version = canary.Version, RunnerThread = new Thread(r), Continue = true });
                            runners[canary.CanaryName].RunnerThread.Start(canary);
                        }
                        else if (runners[canary.CanaryName].Version != canary.Version)
                        {
                            runners[canary.CanaryName].Continue = false;
                            if (runners[canary.CanaryName].CleanUp == null || runners[canary.CanaryName].CleanUp.Result)
                            {
                                runners[canary.CanaryName].CleanUp = Task.Run<bool>(() => {
                                    while (runners[canary.CanaryName].RunnerThread.IsAlive)
                                    {
                                        Thread.Sleep(1000);
                                    }

                                    runners[canary.CanaryName].Version = canary.Version;
                                    runners[canary.CanaryName].Continue = true;
                                    runners[canary.CanaryName].RunnerThread = new Thread(r);
                                    runners[canary.CanaryName].RunnerThread.Start(canary);
                                    return true;
                                });
                            }
                        }
                        else if (!runners[canary.CanaryName].RunnerThread.IsAlive)
                        {
                            // ToDo: report missing canary

                            runners[canary.CanaryName].Version = canary.Version;
                            runners[canary.CanaryName].Continue = true;
                            runners[canary.CanaryName].RunnerThread = new Thread(r);
                            runners[canary.CanaryName].RunnerThread.Start(canary);
                        }
                    }

                    elapsed = DateTime.UtcNow - timer;
                    telemetryClient.TrackMetric(new MetricTelemetry("CanaryService", "Latency", 1, elapsed.TotalMilliseconds, elapsed.TotalMilliseconds, elapsed.TotalMilliseconds, 0.0));
                    telemetryClient.TrackMetric(new MetricTelemetry("CanaryService", "CanariesLoaded", 1, canaries.Count , canaries.Count, canaries.Count, 0.0));
                    Thread.Sleep(5000);

                    elapsed = DateTime.UtcNow - timer;
                    int sleep_time = 10000 - (int)elapsed.TotalSeconds;
                    sleep_time = sleep_time < 0 ? 0 : sleep_time;
                    Thread.Sleep(sleep_time);
                }
            }

        }

        private static void HttpCanaryRun(object o)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddApplicationInsightsTelemetryWorkerService("18548be7-b8c9-422c-a96a-4902073b92ef");

            // Build ServiceProvider.
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var _telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

            int status = 999;
            if (!(o is Canary))
            {
                using (_telemetryClient.StartOperation<RequestTelemetry>("RunnerFailure"))
                {
                    ExceptionTelemetry ext = new ExceptionTelemetry()
                    {
                        Message = $"Object '{nameof(o)}' is expected to be of type 'Canary'",
                        SeverityLevel = SeverityLevel.Error,
                        Timestamp = DateTime.UtcNow
                    };

                    _telemetryClient.TrackException(ext);
                    Thread.Sleep(5000);
                    _telemetryClient.Flush();
                    throw new ArgumentException($"Object '{nameof(o)}' is expected to be of type 'Canary'");
                }
            }

            Canary canary = (Canary)o;

            using (_telemetryClient.StartOperation<RequestTelemetry>(canary.CanaryName))
            {
                string verb = ((string)canary.Test.verb).ToUpperInvariant();
                string url = (string)canary.Test.url;
                string payload = (string)canary.Test.payload;
                int lowest_httpcode = int.Parse((string)canary.Test.lowest_httpcode);
                int highest_httpcode = int.Parse((string)canary.Test.highest_httpcode);
                int timeout = int.Parse((string)canary.Test.timeout);
                Dictionary<string, string> _properties = ((IEnumerable<KeyValuePair<string, JToken>>)canary.Properties).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString()); //(Dictionary<string, string>)canary.Properties;
                string[] dimensionHierarchy = canary.DimensionHierarchy;

                DateTime timer = DateTime.Now;
                HttpResponseMessage response = null;
                StringContent content = null;

                while (runners[canary.CanaryName].Continue)
                {
                    response = null;
                    content = null;
                    timer = DateTime.Now;

                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromMilliseconds(timeout);

                        try
                        {
                            switch (verb)
                            {
                                case "GET":
                                    response = client.GetAsync(url).Result;
                                    break;

                                case "POST":
                                    content = new StringContent(payload, Encoding.UTF8, "application/json");
                                    response = client.PostAsync(url, content).Result;
                                    break;

                                case "PUT":
                                    content = new StringContent(payload, Encoding.UTF8, "application/json");
                                    response = client.PutAsync(url, content).Result;
                                    break;

                                default:
                                    break;
                            }

                            status = (int)response.StatusCode;
                        }
                        catch (AggregateException aggx)  // A connection attempt failed because the connected party did not properly respond after a period of time, 
                                                      // or established connection failed because connected host has failed to respond. 
                                                      // (changemanager.fcm.azure.microsoft.com:443)
                        {
                            _telemetryClient.TrackTrace($"{{ \"MetricNameSpace\": \"{canary.CanaryName}\", \"ExceptionType\": \"AggregateException\", \"StatusCode\": \"901\", \"ErrorMessage\": \"{aggx.Message}\" }}");
                            status = 901;
                        }
                        catch (SocketException sktx)  // A connection attempt failed because the connected party did not properly respond after a period of time, 
                                                 // or established connection failed because connected host has failed to respond. 
                                                 // (changemanager.fcm.azure.microsoft.com:443)
                        {
                            _telemetryClient.TrackTrace($"{{ \"MetricNameSpace\": \"{canary.CanaryName}\", \"ExceptionType\": \"SocketException\", \"StatusCode\": \"902\", \"ErrorMessage\": \"{sktx.Message}\" }}");
                            status = 902;
                        }
                        catch (HttpRequestException httpx)
                        {
                            status = (int)httpx.StatusCode;
                            _telemetryClient.TrackTrace($"{{ \"MetricNameSpace\": \"{canary.CanaryName}\", \"ExceptionType\": \"HttpRequestException\", \"StatusCode\": \"{status}\", \"ErrorMessage\": \"{httpx.Message}\" }}");
                        }
                        catch (Exception ex)
                        {
                            _telemetryClient.TrackTrace($"{{ \"MetricNameSpace\": \"{canary.CanaryName}\", \"ExceptionType\": \"HttpRequestException\", \"StatusCode\": \"999\", \"ErrorMessage\": \"{ex.Message}\" }}");
                            _telemetryClient.TrackException(ex);
                            throw;
                        }

                        TimeSpan elapsed = DateTime.Now - timer;

                        bool canary_success = lowest_httpcode <= status && status <= highest_httpcode ? true : false;

                        int sleep_time = (3600000 / canary.Frequency_Hour) - (int)elapsed.TotalMilliseconds;
                        sleep_time = sleep_time < 0 ? 0 : sleep_time;
                        var Latency_Metric = new MetricTelemetry(canary.CanaryName, "Latency", 1, elapsed.TotalMilliseconds, elapsed.TotalMilliseconds, elapsed.TotalMilliseconds, 0.0);
                        _telemetryClient.TrackMetric(Latency_Metric);
                        _telemetryClient.TrackMetric(new MetricTelemetry(canary.CanaryName, "SUCCEED", 1, canary_success ? 1 : 0, canary_success ? 1 : 0, canary_success ? 1 : 0, 0.0));
                        _telemetryClient.TrackMetric(new MetricTelemetry(canary.CanaryName, "FAIL", 1, canary_success ? 0 : 1, canary_success ? 0 : 1, canary_success ? 0 : 1, 0.0));
                        _telemetryClient.TrackAvailability(canary.CanaryName, DateTime.UtcNow, elapsed, string.Empty, canary_success);

                        //Console.WriteLine($"{DateTime.UtcNow:O} {canary.CanaryName,-40} {url,-120} {verb,-6} {canary_success} {elapsed.TotalMilliseconds,8:N0} {sleep_time,8:N0}");

                        Thread.Sleep(sleep_time);
                        _telemetryClient.Flush();
                    }
                }
            }
        }

        private static void SqlCnxCanaryRun(Object o)
        {
            if (!(o is Canary))
            {
                throw new ArgumentException($"Object '{nameof(o)}' is expected to be of type 'Canary'");
            }

            Canary canary = (Canary)o;

        }

        private static void SqlStmtCanaryRun(Object o)
        {
            if (!(o is Canary))
            {
                throw new ArgumentException($"Object '{nameof(o)}' is expected to be of type 'Canary'");
            }

            Canary canary = (Canary)o;

        }
    }
}
