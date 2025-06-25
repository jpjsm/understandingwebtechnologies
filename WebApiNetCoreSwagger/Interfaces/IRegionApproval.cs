using System;
using System.Collections.Generic;
using System.Text;

namespace FcmHipri.Interfaces
{
    public interface IRegionApproval
    {
        public string Region { get; set; }
        public string[] EventsInPlace { get; set; }
        public Boolean DeploymentApproved { get; set; }

    }
}
