using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq; 
using System.Threading;

namespace Stressing
{
    public enum ThreadStatus
    { 
        NotStarted,
        Running,
        Terminated
    }

    public static class Utils
    {
        public static int PseudoCryptoRandom()
        {
            return BitConverter.ToInt32(Guid.NewGuid().ToByteArray()[2..6], 0);
        }

        public static int PseudoCryptoRandom(int max)
        {
            int r;
            max = max == int.MinValue ? int.MaxValue : max; // Prevent overflow when taking negative in next step
            max = max < 0 ? -max : max; // take abs(max)
            int ceiling = (int.MaxValue / max) * max;
            do
            {
                r = PseudoCryptoRandom() & int.MaxValue;
            } while (r >= ceiling);

            return r % max;
        }
    }
    public class ThreadTemplate
    {
        private string label;
        private int _maxrand;
        private ThreadStatus status = ThreadStatus.NotStarted;
        private long attempts = 0;
        private Dictionary<int, long> distribution = new Dictionary<int, long>();
        private TimeSpan elapsed = TimeSpan.Zero; 
        private Single _reportThreshold;

        public ThreadStatus Status => status;
        public long Attempts => attempts;
        public TimeSpan Elapsed => elapsed;
        public IReadOnlyDictionary<int, long> Distribution => distribution;

        public static ConcurrentDictionary<Guid, ThreadStatus> ThreadSummary = new ConcurrentDictionary<Guid, ThreadStatus>();

        public ThreadTemplate(string s, int maxRand, Single reportThreshold)
        {
            label = s;
            _maxrand = maxRand;
            _reportThreshold = reportThreshold <= 0 || reportThreshold >=1.0 ? 0.01f : reportThreshold;
        }

        public void ResourceMonitor()
        {
            Console.WriteLine("Started resource monitor");
            bool anyActive = ThreadSummary.Values.Any(s => s != ThreadStatus.Terminated);
            int processorCount = Environment.ProcessorCount;
            while (anyActive)
            {
                DateTime start = DateTime.Now;
                TimeSpan initialCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
                Thread.Sleep(100);
                DateTime end = DateTime.Now;
                TimeSpan finalCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

                TimeSpan totalCpuUsed = (finalCpuUsage - initialCpuUsage);
                TimeSpan elapsedTime = (end - start);

                double avgCpuUsed = 100.0 * totalCpuUsed.TotalMilliseconds / (processorCount * elapsedTime.TotalMilliseconds);

                Console.WriteLine($"Average CPU used: {avgCpuUsed:N2} / sec");

                anyActive = ThreadSummary.Values.Any(s => s != ThreadStatus.Terminated);
            }

        }

        public void ThreadTask()
        {
            Guid taskId = Guid.NewGuid();
            ThreadSummary.TryAdd(taskId, Status);

            int threshold = Convert.ToInt32(Math.Round(_maxrand * _reportThreshold, MidpointRounding.ToEven));
            threshold = threshold == 0 ? 1 : threshold;
            status = ThreadStatus.Running;
            ThreadSummary[taskId] = Status;

            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.WriteLine($"{DateTime.Now.ToString("O")} Starting '{label}' with MaxRandom: {_maxrand}");
            int i = Utils.PseudoCryptoRandom(_maxrand);
            int max = int.MinValue;
            int min = int.MaxValue;
            
            while (i != 0)
            {
                if (i > max)
                {
                    max = i;
                }

                if (i < min)
                {
                    min = i;
                }

                if (i <= threshold)
                {
                    //Console.WriteLine($"{DateTime.Now.ToString("O")} Still working on '{label}', attempts so far: {attempts,18:N0}, [min, max]: {min,3:N0}, {max,18:N0}");
                }

                attempts++;
                i = Utils.PseudoCryptoRandom(_maxrand);
            }

            stopwatch.Stop();
            elapsed = new TimeSpan(stopwatch.ElapsedTicks);

            //Console.WriteLine(
            //    $"{DateTime.Now.ToString("O")} Done with '{label}'" + 
            //    $"{Environment.NewLine}--> Attempts total: {attempts}" + 
            //    $"{Environment.NewLine}--> Elapsed time: {stopwatch.ElapsedMilliseconds/1000.0:N3} secs");
            status = ThreadStatus.Terminated;
            ThreadSummary[taskId] = Status;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, long> distribution = new Dictionary<int, long>();
            int maxTasks = 512;
            int maxRandInt = 10000000;
            float reportingThreshold = 0.0000002f;
            Console.WriteLine($"[{DateTime.Now.ToString("O")}]Starting Stressing with {maxTasks} tasks!");

            ThreadTemplate[] tasks = new ThreadTemplate[maxTasks];
            Thread[] threads = new Thread[maxTasks];
            for (int i = 0; i < maxTasks; i++)
            {
                tasks[i] = new ThreadTemplate($"Task {i,6:N0}", maxRandInt, reportingThreshold);
                threads[i] = new Thread(new ThreadStart(tasks[i].ThreadTask));
                threads[i].IsBackground = true;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < maxTasks; i++)
            {
                threads[i].Start();
            }

            ThreadTemplate resourceMonitor = new ThreadTemplate("Resource Monitor", 0, 0.0f);
            Thread rm = new Thread(new ThreadStart(resourceMonitor.ResourceMonitor));
            rm.IsBackground = false;
            rm.Priority = ThreadPriority.AboveNormal;
            rm.Start();

        RESTART:
            Thread.Sleep(5000);
            bool done = true;
            for (int i = 0; i < maxTasks; i++)
            {
                if (tasks[i].Status != ThreadStatus.Terminated)
                {
                    done = false;
                }
                else
                {
                    //Console.WriteLine($"Task[{i,3}] finished in {tasks[i].Elapsed.TotalSeconds,10:N3}, after {tasks[i].Attempts,10:N0}.");
                }
            }

            if (!done)
            {
                //Console.WriteLine($"Main thread back to nap.");
                goto RESTART;
            }

            stopwatch.Stop();

            Console.WriteLine($"{new string('=', 36)} Summary {new string('=', 36)}");
            for (int i = 0; i < maxTasks; i++)
            {
                //Console.WriteLine($"Task[{i,3}] finished in {tasks[i].Elapsed.TotalSeconds,10:N3}, after {tasks[i].Attempts,10:N0}.");
                foreach (KeyValuePair<int, long> kvp in tasks[i].Distribution)
                {
                    if (!distribution.ContainsKey(kvp.Key))
                    {
                        distribution.Add(kvp.Key, 0);
                    }

                    distribution[kvp.Key] += kvp.Value;
                }
            }

            Console.WriteLine($"[{DateTime.Now.ToString("O")}]Completed Stressing with {maxTasks} tasks, in {stopwatch.Elapsed.TotalSeconds,8:N0} seconds!");
            return;
            Console.WriteLine();
            Console.WriteLine($"{new string('=', 36)} Frequency Table {new string('=', 36)}");
            Console.WriteLine();
            Console.WriteLine($"{"Label",10} {"Count",10}");

            for (int i = 0; i < maxTasks; i++)
            {
                foreach (KeyValuePair<int, long> kvp in tasks[i].Distribution.OrderBy(v => v.Key))
                {
                    Console.WriteLine($"{kvp.Key,10:N0} {kvp.Value,10:N0}");
                }
            }


        }
    }
}
