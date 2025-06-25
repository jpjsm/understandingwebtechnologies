using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;

namespace FcmHipri.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }
        public string[] Regions { get; set; }
    }
}
