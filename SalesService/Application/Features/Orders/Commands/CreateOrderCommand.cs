using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Commands
{
    public class CreateOrderCommand : ICreateCommand, IAuthenticationRequest
    {
        public string BillingAccountId { get; set; }
    }
}
