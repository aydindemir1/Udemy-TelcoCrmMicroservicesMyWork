using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerIdAsync([FromRoute] string customerId)
        {
            var query = new GetOrdersByCustomerIdQuery { CustomerId = customerId };
            return Ok(await CqrsProcessor.SendAsync(query));
        }
    }
}
