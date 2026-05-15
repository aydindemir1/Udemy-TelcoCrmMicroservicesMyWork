using Application.Features.ProductOfferings.Commands.Create;
using Application.Features.ProductOfferings.Queries.GetList;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOfferingsController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductOfferingCommand command)
        {
            var result = await CqrsProcessor.SendAsync(command);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await CqrsProcessor.SendAsync(new GetListProductOfferingQuery());
            return Ok(result);
        }
    }
}
