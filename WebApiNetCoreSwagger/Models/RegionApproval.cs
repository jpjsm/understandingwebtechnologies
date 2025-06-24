using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;
namespace FcmHipri.Models
{
    public class RegionApproval
    {
        public string Region { get; set; }
        public string[] EventsInPlace { get; set; }
        public bool DeploymentApproved { get; set; }
    }
}
