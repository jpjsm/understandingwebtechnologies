using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class ExceptionRequestBody
    {
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public Deployment deployment { get; set; }
    }
}
