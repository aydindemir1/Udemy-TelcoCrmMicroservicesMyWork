using Application.Features.Cities.Commands.Create;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController
    {


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateCityCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }

    }
}
