using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiControllerAttribute.Models;

namespace WebApiControllerAttribute.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Get();
        Order Get(Guid orderId);
        void Add(Order order);
        void Update(Guid orderId, Order order);
        Order Delete(Guid orderId);
    }
}
