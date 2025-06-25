using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiFilters.Filters;
using WebApiFilters.Models;
using WebApiFilters.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFilters.Controllers
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
        [OrderExists]
        [HttpGet("{id}")]
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
        [OrderExists]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Order request)
        {
            if (request.ItemIds == null)
            {
                return BadRequest(new { Message = $"ItemIds cannot be null." });
            }

            var order = _orderRepository.Get(id);

            // Following code isn't needed because of [OrderExists] filter
            //if (order == null)
            //{
            //    return NotFound(new { Message = $"Order with id: '{id}' does not exist." });
            //}


            order.CopyOrderDetails(request);
            _orderRepository.Update(id, order);
            return Ok();
        }

        // PATCH api/<OrderController>/5
        [OrderExists]
        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, JsonPatchDocument<Order> requestOp)
        {
            var order = _orderRepository.Get(id);

            // Following code isn't needed because of [OrderExists] filter
            //if (order == null)
            //{
            //    return NotFound(new { Message = $"Order with id: '{id}' does not exist." });
            //}

            requestOp.ApplyTo(order);
            _orderRepository.Update(id, order);
            return Ok();
        }

        // DELETE api/<OrderController>/5
        [OrderExists]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Following code isn't needed because of [OrderExists] filter
            //var order = _orderRepository.Get(id);

            //if (order == null)
            //{
            //    return NotFound(new { Message = $"Order with id: '{id}' does not exist." });
            //}

            _orderRepository.Delete(id);
            return NoContent();
        }
    }
}
