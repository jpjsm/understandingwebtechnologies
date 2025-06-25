using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Interfaces;
using FcmHipri.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FcmHipri.Controllers
{
    [Route("DeploymentAuthorization")]
    [ApiController]
    public class DeploymentAuthorizationController : ControllerBase
    {
        //// GET: api/<DeploymentApprovalController>
        //[HttpGet]
        //public IEnumerable<DeploymentAuthorization> Get(
        //    [FromQuery]Guid ServiceId, 
        //    [FromQuery]string RequestorEmail, 
        //    [FromQuery]Guid[] SubscriptionIds,
        //    [FromQuery]ExceptionRequestStatus[] Statuses,
        //    [FromQuery]DateTime RequestedStartFrom, 
        //    [FromQuery]DateTime RequestedUpTo)
        //{
        //    return new DeploymentAuthorization[] { (DeploymentAuthorization)null, (DeploymentAuthorization)null };
        //}

        //// GET api/<DeploymentApprovalController>/5
        //[HttpGet("{id}")]
        //public DeploymentAuthorization Get(Guid id)
        //{
        //    return (DeploymentAuthorization)null;
        //}

        // POST api/<DeploymentApprovalController>
        [HttpPost]
        public DeploymentAuthorization Post(Deployment deployment)
        {
            return (DeploymentAuthorization)null;
        }

    }
}
