using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.Delete
{
    public class DeleteIndividualCustomerCommand : IDeleteCommand, IAuthenticationRequest
    {
        public Guid Id { get; set; }
    }
}
