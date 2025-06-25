using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FcmHipri.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FcmHipri.Controllers
{
    [Route("EventsRetrieval")]
    [ApiController]
    public class EventsRetrievalController : ControllerBase
    {
        // GET: api/<EventsRetrievalController>
        [HttpGet]
        public IEnumerable<EventsInPlace> Get(EventsQuery query)
        {
            return new EventsInPlace[] { (EventsInPlace)null, (EventsInPlace)null };
        }
    }
}
