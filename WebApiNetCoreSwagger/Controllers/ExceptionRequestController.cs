using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FcmHipri.Models;
using FcmHipri.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FcmHipri.Controllers
{
    [Route("ExceptionRequest")]
    [ApiController]
    public class ExceptionRequestController : ControllerBase
    {
        private IEnumerable<ExceptionRequest> _exreqs = new ExceptionRequest[]{};
        // GET: api/<ExceptionRequestController>
        [HttpGet]
        public IEnumerable<ExceptionRequest> Get(
            [FromQuery] Guid[] ServiceIds,
            [FromQuery] string ApproverEmail,
            [FromQuery] string RequestorEmail,
            [FromQuery] Guid[] SubscriptionIds,
            [FromQuery] ExceptionRequestStatus[] statuses,
            [FromQuery] DateTime? RequestedStartFrom,
            [FromQuery] DateTime? RequestedUpTo
            )
        {
            IEnumerable<ExceptionRequest> results = _exreqs;
            if (ServiceIds.Length > 0)
            {
                results = results.Where(r => ServiceIds.Contains(r.Deployment.ServiceId));
            }

            if (!string.IsNullOrWhiteSpace(ApproverEmail))
            {
                results = results.Where(r => r.ApproverEmail == ApproverEmail);
            }

            if (!string.IsNullOrWhiteSpace(ApproverEmail))
            {
                results = results.Where(r => r.RequestorEmail == RequestorEmail);
            }

            if (SubscriptionIds.Length > 0)
            {
                results = results.Where(r => r.Deployment.Subscriptions.Select(s => s.SubscriptionId).Intersect(SubscriptionIds).Any());
            }

            if (statuses.Length > 0)
            {
                results = results.Where(r => statuses.Contains(r.Status));
            }

            if (RequestedStartFrom != null)
            {
                results = results.Where(r => r.RequestDate >= RequestedStartFrom);
            }
            else
            {
                results = results.Where(r => r.RequestDate >= DateTime.UtcNow.AddDays(-7.0));
            }

            if (RequestedUpTo != null)
            {
                results = results.Where(r => r.RequestDate <= RequestedUpTo);
            }

            return results;
        }

        // GET api/<ExceptionRequestController>/5
        [HttpGet("{exceptionRequestId}")]
        public ExceptionRequest Get(Guid exceptionRequestId)
        {
            return (ExceptionRequest)null;
        }

        // POST api/<ExceptionRequestController>
        [Route("fromdeclined")]
        [HttpPost]
        public IEnumerable<ExceptionRequest> Post(DeploymentAuthorization declined)
        {
            return _exreqs;
        }

        // POST api/<ExceptionRequestController>
        [Route("preapproval")]
        [HttpPost]
        public IEnumerable<ExceptionRequest> Post(ExceptionRequestBody body)
        {
            return _exreqs;
        }

        // PUT api/<ExceptionRequestController>/5
        [HttpPut("{exceptionRequestId}")]
        public void Put(Guid exceptionRequestId, ExceptionRequest updatedRequest)
        {

        }

        //// DELETE api/<ExceptionRequestController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
