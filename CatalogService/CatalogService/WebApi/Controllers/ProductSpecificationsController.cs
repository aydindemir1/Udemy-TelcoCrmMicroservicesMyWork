using Application.Features.ProductSpecifications.Commands;
using Application.Features.ProductSpecifications.Queries.GetList;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductSpecificationCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetListProductSpecificationQuery { Id = id };

            var result = await CqrsProcessor.SendAsync(query);

            return Ok(result);
        }
    }
}
