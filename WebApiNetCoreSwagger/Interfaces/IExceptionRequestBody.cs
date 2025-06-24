using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FcmHipri.Interfaces
{
    interface IExceptionRequestBody
    {
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public IDeployment deployment { get; set; }
    }
}
