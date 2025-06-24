using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;
namespace FcmHipri.Models
{
    public class SubscriptionApproval
    {
        public Guid SubscriptionId { get; set; }
        public string[] EventsInPlace { get; set; }
        public bool DeploymentApprovedForEntireSubscription { get; set; }
        public RegionApproval[] Regions { get; set; }
        public Guid[] ExceptionRequests { get; set; }
    }
}
