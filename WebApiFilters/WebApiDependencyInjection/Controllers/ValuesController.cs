using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private IPaymentService paymentService { get; set; }

        public ValuesController(IPaymentService paymentsvc)
        {
            paymentService = paymentsvc;
        }
        public string Get()
        {
            return paymentService.GetMessage();
        }
    }
}
