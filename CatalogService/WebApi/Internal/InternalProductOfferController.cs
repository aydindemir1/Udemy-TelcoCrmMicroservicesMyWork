using Application.Features.ProductOfferings.Queries.Internal.GetById;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Internal
{
    [Route("api/internal/productoffers")]
    [ApiController]
    public class InternalProductOfferController : BaseController
    {
        [HttpGet("{productOfferId}")]
        public async Task<IActionResult> GetById(Guid productOfferId)
        {
            var query = (new GetInternalProductOfferByIdQuery() { Id = productOfferId });
            return Ok(await CqrsProcessor.SendAsync(query));
        }
    }
}
