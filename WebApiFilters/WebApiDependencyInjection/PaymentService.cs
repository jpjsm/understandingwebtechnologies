using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDependencyInjection
{
    public interface IPaymentService
    {
        string GetMessage();
    }
    public class PaymentService : IPaymentService
    {
        public string GetMessage() => "Pay me! I'm a developer in test ;)";
    }
    public class ExternalPaymentService : IPaymentService
    {
        public string GetMessage() => "Pay me!, I'm a customer from a production service!";
    }
}
