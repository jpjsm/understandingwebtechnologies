using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class StatusChange : IStatusChange
    {
        public ExceptionRequestStatus status { get; set; }
        public DateTime changeDate { get; set; }
    }
}
