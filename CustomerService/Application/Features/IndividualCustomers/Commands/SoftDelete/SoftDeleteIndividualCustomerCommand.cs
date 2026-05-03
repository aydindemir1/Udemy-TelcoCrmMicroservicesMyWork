using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.SoftDelete
{
    public class SoftDeleteIndividualCustomerCommand : IDeleteCommand//, IAuthenticationRequest
    {
        public Guid Id { get; set; }
    }
}
