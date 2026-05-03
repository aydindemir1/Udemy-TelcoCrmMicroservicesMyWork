using Application.Features.Districts.Commands.Create;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : BaseController
    {


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateDistrictCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }
    }
}
