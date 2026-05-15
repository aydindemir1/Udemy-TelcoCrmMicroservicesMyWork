using Application.Features.ProductSpecCharacteristics.Commands.Create;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecCharacteristicsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductSpecCharacteristicCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }
    }
}
