using Application.Features.ContactMediums.Commands.Create;
using Application.Features.ContactMediums.Queries.GetListPaginated;
using Core.Cqrs;
using Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMediumsController : BaseController
    {


        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] CreateContactMediumCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }


        [HttpGet]
        public async Task<ActionResult> GetListPaginatedAsync([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            GetListContactMediumQuery query = new GetListContactMediumQuery() { PageIndex = pageIndex, PageSize = pageSize };
            ContactMediumListModel responses = await CqrsProcessor.SendAsync(query);
            return Ok(responses);
        }

    }
}
