using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Commands.Create
{
    public class CreateBillingAccountCommand : ICreateCommand<CreatedBillingAccountResponse>, IAuthenticationRequest
    {
        public Guid CustomerId { get; set; }
        public Guid BillingAddressId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BillingAccountType Type { get; set; } // Corporate, Individual, Prepaid
        public BillingAccountStatus Status { get; set; }

    }
}
