using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Commands.Create
{
    public class AddToBasketCommand : ICreateCommand<Unit>, IAuthenticationRequest
    {
        public Guid BillingAccountId { get; set; }
        public Guid ProductOfferId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
