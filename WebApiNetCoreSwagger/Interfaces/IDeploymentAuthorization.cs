using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IDeploymentAuthorization
    {
        public Guid Id { get; }
        public DateTime AuthorizationRequestDate { get; }
        public Guid DeploymentId { get; } // Ev2: RolloutId
        public string Requestor { get; }
        public string RequestorEmail { get; }
        public DateTime DeploymentEstimatedStart { get; }
        public DateTime DeploymentEstimatedEnd { get; }
        public Guid ServiceId { get; set; }
        public ISubscriptionApproval[] Subscriptions { get; }
        public ExceptionRequestStatus Status { get; }
    }
}
