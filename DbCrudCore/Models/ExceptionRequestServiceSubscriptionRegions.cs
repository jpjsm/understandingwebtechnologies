using System;
using System.Collections.Generic;

namespace DbCrudCore.Models
{
    public partial class ExceptionRequestServiceSubscriptionRegions
    {
        public Guid ExceptionRequestId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Region { get; set; }

        public virtual ExceptionRequests ExceptionRequest { get; set; }
    }
}
