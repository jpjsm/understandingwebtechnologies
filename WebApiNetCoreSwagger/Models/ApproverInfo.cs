using FcmHipri.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FcmHipri.Models
{
    public class ApproverInfo
    {
        public Guid ApproverId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }
        public Guid[] ServiceIds { get; set; }
    }
}
