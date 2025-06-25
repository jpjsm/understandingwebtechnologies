using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class ApproverNotes : IApproverNotes
    {
        public Guid ApproverId { get; set; }
        public string Notes { get; set; }
    }
}
