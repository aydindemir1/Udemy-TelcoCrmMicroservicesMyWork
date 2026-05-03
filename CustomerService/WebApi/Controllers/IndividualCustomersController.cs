using Application.Features.IndividualCustomers.Commands.Create;
using Application.Features.IndividualCustomers.Commands.Delete;
using Application.Features.IndividualCustomers.Commands.SoftDelete;
using Application.Features.IndividualCustomers.Queries.GetList;
using Core.Cqrs;
using Core.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualCustomersController : BaseController
    {
        //Eski Yapı

        //private readonly IMediator _mediator;

        //public IndividualCustomersController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] CreateIndividualCustomerCommand command)
        {
            return Created("", await CqrsProcessor.SendAsync(command));
        }

        [HttpGet]
        public async Task<ActionResult> GetListAsync([FromQuery] GetListIndividualCustomerQuery query)
        {
            var result = await CqrsProcessor.SendAsync(query);
            return Ok(result);
        }

        [HttpDelete("softdelete/{id}")]
        public async Task<ActionResult> SoftDeleteAsync([FromRoute] Guid id)
        {
            SoftDeleteIndividualCustomerCommand command = new SoftDeleteIndividualCustomerCommand() { Id = id };
            return Ok(await CqrsProcessor.SendAsync(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            DeleteIndividualCustomerCommand command = new DeleteIndividualCustomerCommand() { Id = id };
            return Ok(await CqrsProcessor.SendAsync(command));
        }

    }
}
