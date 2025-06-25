using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiFilters.Repositories;

namespace WebApiFilters.Filters
{
    public class OrderExistsAttribute : TypeFilterAttribute
    {
        public OrderExistsAttribute(): base(typeof(OrderExistsFilterImpl))
        {

        }

        private class OrderExistsFilterImpl: IAsyncActionFilter
        {
            private readonly IOrderRepository _orderRepository;

            public OrderExistsFilterImpl(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("id"))
                {
                    context.Result = new BadRequestObjectResult(new { Message = "Does not contain Id" });//new BadRequestResult();
                    return;
                }

                if (!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestObjectResult(new { Message = "Id isn't GUID type" });//new BadRequestResult();
                    return;
                }

                var result = _orderRepository.Get(id);

                if (result == null)
                {
                    context.Result = new NotFoundObjectResult(new { Message = $"Order '{id}' does not exist."});
                    return;
                }

                await next();
            }
        }
    }
}
