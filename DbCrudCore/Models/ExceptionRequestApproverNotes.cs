using System;
using System.Collections.Generic;

namespace DbCrudCore.Models
{
    public partial class ExceptionRequestApproverNotes
    {
        public Guid ExceptionRequestId { get; set; }
        public Guid ApproverId { get; set; }
        public string Notes { get; set; }

        public virtual ExceptionRequests ExceptionRequest { get; set; }
    }
}
