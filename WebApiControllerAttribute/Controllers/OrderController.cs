using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiControllerAttribute.Models;
using WebApiControllerAttribute.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiControllerAttribute.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderRepository.Get());
        }

        // GET api/<OrderController>/5
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_orderRepository.Get(id));
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post(Order request)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                ItemIds = request.ItemIds,
                Currency = request.Currency
            };

            _orderRepository.Add(order);
            return CreatedAtAction("Get", "Order", new { id = order.Id }, null);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Order request)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with id: '{id}' does not exist." });
            }

            if (request.ItemIds == null)
            {
                return BadRequest(new { Message = $"ItemIds cannot be null." });
            }

            order.ItemIds = request.ItemIds;
            order.Currency = request.Currency;

            _orderRepository.Update(id, order);
            return Ok();
        }

        // PATCH api/<OrderController>/5
        [HttpPatch("{id:guid}")]
        public IActionResult Patch(Guid id, JsonPatchDocument<Order> requestOp)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with id: '{id}' does not exist." });
            }

            requestOp.ApplyTo(order);
            _orderRepository.Update(id, order);
            return Ok();
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with id: '{id}' does not exist." });
            }

            _orderRepository.Delete(id);
            return NoContent();
        }
    }
}
