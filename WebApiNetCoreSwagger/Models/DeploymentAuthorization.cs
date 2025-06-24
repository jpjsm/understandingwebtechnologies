using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class DeploymentAuthorization
    {
        public Guid Id { get; }

        public DateTime AuthorizationRequestDate { get; }

        public Guid DeploymentId { get; }

        public string Requestor { get; set; }

        public string RequestorEmail { get; set; }

        public DateTime DeploymentEstimatedStart { get; }

        public DateTime DeploymentEstimatedEnd { get; }

        public Guid ServiceId { get; set; }

        public SubscriptionApproval[] Subscriptions { get; set; }

        public ExceptionRequestStatus Status { get; set; }

        public DeploymentAuthorization(Deployment deployment)
        {

        }

        private void Map(Deployment other) 
        { 
        }
    }
}
