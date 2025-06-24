using System;
using System.Collections.Generic;

namespace DbCrudCore.Models
{
    public partial class ExceptionRequestStatusChange
    {
        public Guid ExceptionRequestId { get; set; }
        public string Status { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual ExceptionRequests ExceptionRequest { get; set; }
    }
}
