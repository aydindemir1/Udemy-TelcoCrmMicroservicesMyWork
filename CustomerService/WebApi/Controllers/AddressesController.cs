using Application.Features.Addresses.Commands.Create;
using Application.Features.Addresses.Queries.GetAddressByCustomerId;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : BaseController
    {


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateAddressCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetByCustomerId([FromRoute] Guid customerId)
        {
            var query = new GetAddressByCustomerIdQuery { CustomerId = customerId };
            var result = await CqrsProcessor.SendAsync(query);
            return Ok(result);
        }
    }
}
