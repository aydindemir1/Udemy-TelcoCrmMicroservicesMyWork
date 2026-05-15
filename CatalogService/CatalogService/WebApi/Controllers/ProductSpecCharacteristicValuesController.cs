using Application.Features.ProductSpecCharacteristicValues.Commands.Create;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecCharacteristicValuesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductSpecCharacteristicValueCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }
    }
}
