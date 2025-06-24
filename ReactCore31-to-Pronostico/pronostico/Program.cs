using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace pronostico
{
    public class Program
    {
        private static string[] _environmentVariableNames = new string[] {
            "pronosticoBackend_BaseAddress"
        };

        public static Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            foreach (string key in _environmentVariableNames)
            {
                EnvironmentVariables.Add(key, Environment.GetEnvironmentVariable(key));
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
