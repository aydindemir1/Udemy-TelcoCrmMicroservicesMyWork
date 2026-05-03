using Application.Features.BillingAccounts.Commands.Create;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingAccountsController : BaseController
    {


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateBillingAccountCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }
    }
}
