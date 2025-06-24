using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FcmHipri.Interfaces
{
    public interface IApproverNotes
    {
        public Guid ApproverId { get; set; }
        public string Notes { get; set; }
    }
}
