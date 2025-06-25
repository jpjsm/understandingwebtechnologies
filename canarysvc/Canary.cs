using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canarysvc
{
    public class Canary
    {
        public string Kind { get; set; }
        public string CanaryName { get; set; }
        public string Version { get; set; }
        public dynamic Properties { get; set; }
        public string[] DimensionHierarchy { get; set; }
        public dynamic Test { get; set; }
        public int Frequency_Hour { get; set; } = 14400;   //  1000ms: 1 x sec ==  3600;  
                                                           //   500ms: 2 x sec ==  7200; 
                                                           //   333ms: 3 x sec == 10800; 
                                                           //*  250ms: 4 x sec == 14400; *
                                                           //   200ms: 5 x sec == 18000;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
