using Application.Features.Customers.Queries;
using Core.Cqrs;
using Core.ElasticSearch.Models;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSearchController : BaseController
    {
        public async Task<IActionResult> Search([FromQuery] string keyword = "", [FromQuery] int from = 0, [FromQuery] int size = 10)
        {

            var searchParameters = new SearchParameters
            {
                IndexName = "customers",
                Keyword = keyword,
                From = from,
                Size = size
            };

            var query = new CustomerSearchQuery
            {
                SearchParameters = searchParameters
            };
            var result = await CqrsProcessor.SendAsync(query);
            return Ok(result);
        }
    }
}
