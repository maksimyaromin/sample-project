using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linnworks.Web.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrdersService _ordersService;
        private readonly IOrderPrioritiesService _orderPrioritiesService;

        public OrdersController(
            IOrdersService ordersService,
            IOrderPrioritiesService orderPrioritiesService)
        {
            _ordersService = ordersService;
            _orderPrioritiesService = orderPrioritiesService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] OrderDto order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _ordersService.UpdateAsync(id, order, HttpContext.RequestAborted);

            return NoContent();
        }

        [HttpGet("priorities")]
        public async Task<ActionResult<IEnumerable<OrderPriorityDto>>> GetOrderPrioritiesAsync()
        {
            return Ok(await _orderPrioritiesService.SearchAsync(HttpContext.RequestAborted));
        }
    }
}
