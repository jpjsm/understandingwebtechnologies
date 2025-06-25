using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface ISubscriptionApproval
    {
        public Guid SubscriptionId { get; set; }
        public string[] EventsInPlace { get; set; }
        public Boolean DeploymentApprovedForEntireSubscription { get; set; }
        public IRegionApproval[] Regions { get; set; }
        public Guid[] ExceptionRequests { get; set; }
    }
}
