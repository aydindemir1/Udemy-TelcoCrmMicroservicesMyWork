using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Internal
{
    [Route("api/internal/accounts")]
    [ApiController]
    public class InternalBillingAccountController : BaseController
    {
        //[HttpGet("{accountId}")]
        //public async Task<IActionResult> GetById(Guid accountId)
        //{
        //   // GetInternalBillingAccountByIdQuery query = new GetInternalBillingAccountByIdQuery() { Id = accountId };
        //    //return Ok(await CqrsProcessor.SendAsync(query));
        //}
    }
}
