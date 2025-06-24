using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FcmHipri.Controllers
{
    [Route("Approvers")]
    [ApiController]
    public class ApproversController : ControllerBase
    {
        // GET: api/<ApproversController>
        [HttpGet]
        public IEnumerable<ApproverInfo> Get(ApproversQry query)
        {
            return new ApproverInfo[] { (ApproverInfo)null, (ApproverInfo)null };
        }
    }
}
