using Application.Features.ProductOfferingPrices.Commands.Create;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOfferingPricesController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductOfferingPriceCommand command)
        {
            var result = await CqrsProcessor.SendAsync(command);
            return Created("", result);
        }
    }
}
