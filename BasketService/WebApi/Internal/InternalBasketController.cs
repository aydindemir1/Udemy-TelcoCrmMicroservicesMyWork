using Application.Features.Baskets.Queries.Internal.GetInternalByBillingAccount;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Internal
{
    [Route("api/internal/baskets")]
    [ApiController]
    public class InternalBasketController : BaseController
    {

        [HttpGet("{billingAccountId}")]
        public async Task<IActionResult> GetByBillingAccountId(Guid billingAccountId)
        {
            var query = new GetInternalBasketByBillingAccountQuery() { BillingAccountId = billingAccountId };
            return Ok(await CqrsProcessor.SendAsync(query));

        }
    }
}
