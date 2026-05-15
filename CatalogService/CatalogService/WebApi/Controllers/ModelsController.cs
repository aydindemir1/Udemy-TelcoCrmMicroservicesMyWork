using Application.Features.Models.Commands;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateModelCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }
    }
}
