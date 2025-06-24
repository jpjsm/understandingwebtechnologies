using System;
using System.Collections.Generic;

namespace EFFromData.DbModel
{
    public partial class ExceptionRequestApproverNotes
    {
        public Guid ExceptionRequestId { get; set; }
        public Guid ApproverId { get; set; }
        public string Notes { get; set; }

        public virtual ExceptionRequestTable ExceptionRequest { get; set; }
    }
}
