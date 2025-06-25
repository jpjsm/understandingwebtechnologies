using System;
using System.Collections.Generic;

namespace EFFromData.DbModel
{
    public partial class ExceptionRequestTable
    {
        public ExceptionRequestTable()
        {
            ExceptionRequestApproverNotes = new HashSet<ExceptionRequestApproverNotes>();
            ExceptionRequestFormerApproverIds = new HashSet<ExceptionRequestFormerApproverIds>();
            ExceptionRequestServiceSubscriptionRegions = new HashSet<ExceptionRequestServiceSubscriptionRegions>();
            ExceptionRequestStatusChange = new HashSet<ExceptionRequestStatusChange>();
        }

        public Guid ExceptionRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string BusinessJustification { get; set; }
        public DateTime ExceptionBeginsOn { get; set; }
        public DateTime ExceptionEndsOn { get; set; }
        public Guid? DeploymentOfOrigin { get; set; }
        public string ExceptionRequestStatus { get; set; }
        public Guid? ApproverId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }

        public virtual ICollection<ExceptionRequestApproverNotes> ExceptionRequestApproverNotes { get; set; }
        public virtual ICollection<ExceptionRequestFormerApproverIds> ExceptionRequestFormerApproverIds { get; set; }
        public virtual ICollection<ExceptionRequestServiceSubscriptionRegions> ExceptionRequestServiceSubscriptionRegions { get; set; }
        public virtual ICollection<ExceptionRequestStatusChange> ExceptionRequestStatusChange { get; set; }
    }
}
