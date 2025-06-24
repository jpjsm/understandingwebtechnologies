using System;
using System.Collections.Generic;

namespace FcmHipri.Interfaces
{
    public interface IDeploymentProgress
    {
        public IDeploymentAuthorization IsDeploymentApproved(Guid deploymentId);
        public IDeploymentAuthorization IsDeploymentApproved(IDeployment deployment);
        public IDeploymentAuthorization[] AreDeploymentsApproved(IEnumerable<Guid> deploymentIds);
        public ISubscriptionApproval IsSubscriptionExceptionApproved(ISubscription subscription, DateTime DeploymentEstimatedStart, DateTime DeploymentEstimatedEnd);
        public ISubscriptionApproval[] AreSubscriptionExceptionsApproved(IEnumerable<ISubscription> subscriptions, DateTime DeploymentEstimatedStart, DateTime DeploymentEstimatedEnd);
    }
}
