using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IExceptionRequest
    {
        public Guid ExceptionRequestId { get; }
        public DateTime RequestDate { get; }
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string BusinessJustification { get; set; }
        public IDeploymentAuthorization Deployment { get; set; }
        public ExceptionRequestStatus Status { get; }
        public Guid? ApproverId { get; }
        public string ApproverName { get; }
        public string ApproverEmail { get; }
        public Guid[] FormerApproverIds { get; }
        public IStatusChange[] ChangeStatus { get; }
        public IApproverNotes[] ApproversNotes { get; }
    }
}
