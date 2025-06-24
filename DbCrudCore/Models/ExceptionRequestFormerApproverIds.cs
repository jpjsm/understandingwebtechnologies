using System;
using System.Collections.Generic;

namespace DbCrudCore.Models
{
    public partial class ExceptionRequestFormerApproverIds
    {
        public Guid ExceptionRequestId { get; set; }
        public Guid ApproverId { get; set; }

        public virtual ExceptionRequests ExceptionRequest { get; set; }
    }
}
