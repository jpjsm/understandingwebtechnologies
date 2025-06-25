using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IDeployment
    {
        public Guid Id { get; set; } // Ev2: RolloutId
        public string Requestor { get; set; }
        public string RequestorEmail { get; set; }
        public DateTime DeploymentEstimatedStart { get; set; }
        public DateTime DeploymentEstimatedEnd { get; set; }
        public Guid ServiceId { get; set; }
        public ISubscription[] Subscriptions { get; set; }
    }
}
