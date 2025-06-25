using System;
using System.Collections.Generic;

namespace EFFromData.DbModel
{
    public partial class ExceptionRequestStatusChange
    {
        public Guid ExceptionRequestId { get; set; }
        public string Status { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual ExceptionRequestTable ExceptionRequest { get; set; }
    }
}
