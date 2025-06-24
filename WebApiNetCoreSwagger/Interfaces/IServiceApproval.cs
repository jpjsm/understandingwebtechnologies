using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IServiceApproval
    {
        public Guid ServiceId { get; set; }
        public ISubscriptionApproval[] Subscriptions { get; set; }
    }
}
