using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFilters.Models;

namespace WebApiFilters.Repositories
{
    public class MemoryOrderRepository:IOrderRepository
    {
        private IList<Order> _orders { get; set; }
        public MemoryOrderRepository()
        {
            _orders = new List<Order>();
        }

        public IEnumerable<Order> Get()
        {
            return _orders.AsEnumerable();
        }

        public Order Get(Guid orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public void Update(Guid orderId, Order order)
        {
            var result = _orders.FirstOrDefault(o => o.Id == orderId);
            if (result != null)
            {
                result.CopyOrderDetails(order);
            }
        }

        public Order Delete(Guid orderId)
        {
            var target = _orders.FirstOrDefault(o => o.Id == orderId);
            _orders.Remove(target);
            return target;
        }
    }
}
