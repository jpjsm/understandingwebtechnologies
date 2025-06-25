using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace canarysvc
{
    public class Runner
    {
        public string Version { get; set; }
        public Thread RunnerThread { get; set; }

        public bool Continue { get; set; }
        public Task<bool> CleanUp { get; set; }
    }
}
