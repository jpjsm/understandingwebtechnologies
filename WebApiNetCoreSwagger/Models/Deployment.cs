using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class Deployment
    {
        public Guid Id { get; set; }
        public string Requestor { get; set; }
        public string RequestorEmail { get; set; }
        public DateTime DeploymentEstimatedStart { get; set; }
        public DateTime DeploymentEstimatedEnd { get; set; }
        public Guid ServiceId { get; set; }
        public Subscription[] Subscriptions { get; set; }
    }
}
