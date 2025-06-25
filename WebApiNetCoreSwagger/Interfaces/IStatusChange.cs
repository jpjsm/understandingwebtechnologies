using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FcmHipri.Interfaces
{
    public interface IStatusChange
    {
        ExceptionRequestStatus status { get; set; }
        DateTime changeDate { get; set; }
    }
}
