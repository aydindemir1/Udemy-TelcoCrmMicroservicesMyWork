using Application.Features.BasketItems.Commands;
using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Queries.GetByBillingAccountId;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromBody] AddToBasketCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }

        [HttpDelete("{billingAccountId}/item/{basketItemId}")]
        public async Task<IActionResult> RemoveItem(Guid billingAccountId, Guid basketItemId)
        {
            var command = new DeleteBasketItemCommand() { BillingAccountId = billingAccountId, BasketItemId = basketItemId };
            await CqrsProcessor.SendAsync(command);
            return NoContent();
        }

        [HttpDelete("{billingAccountId}")]
        public async Task<IActionResult> ClearBasket(Guid billingAccountId)
        {
            var command = new ClearBasketCommand() { BillingAccountId = billingAccountId };
            await CqrsProcessor.SendAsync(command);
            return NoContent();
        }

        [HttpGet("{billingAccountId}")]
        public async Task<IActionResult> GetByBillingAccountId(Guid billingAccountId)
        {
            var query = new GetBasketByBillingAccountQuery() { BillingAccountId = billingAccountId };
            return Ok(await CqrsProcessor.SendAsync(query));

        }
    }
}
