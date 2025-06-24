using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface ISubscription
    {
        public Guid SubscriptionId { get; set; }
        public string[] Regions { get; set; }
    }
}
