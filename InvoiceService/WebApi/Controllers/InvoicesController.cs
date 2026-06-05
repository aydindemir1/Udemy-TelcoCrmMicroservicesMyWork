using Application.Features.Invoices.Queries;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : BaseController
    {

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetByOrderNumber([FromRoute] string orderNumber)
        {
            var query = new GetInvoiceByOrderNumberQuery { OrderNumber = orderNumber };
            return Ok(await CqrsProcessor.SendAsync(query));
        }
    }
}
