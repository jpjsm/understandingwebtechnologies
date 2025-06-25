using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IService
    {
        public Guid ServiceId { get; set; }
        public ISubscription[] Subscriptions { get; set; }
    }
}
