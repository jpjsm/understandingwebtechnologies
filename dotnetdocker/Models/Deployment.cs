using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Deployment
    {
        // based on https://ev2docs.azure.net/getting-started/authoring/service-model/servicemodel.html
        public string DeploymentId { get; set; }
        public string ServiceGroup { get; set; } // From Service Metadata
        public string Environmet { get; set; } // From Service Metadata
        public string ServiceIdentifier { get; set; } // From Service Metadata
        public string ServiceResourceGroups { get; set; } // From Service resource groups
        public DateTime DeploymentStart { get; set; } // Estimated time to start deployment
        public DateTime DeploymentEnd { get; set; } // Estimated time for the deployment ot finish
    }
}
