using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BasketItems.Commands
{
    public class DeleteBasketItemCommand : IDeleteCommand , IAuthenticationRequest
    {
        public Guid BillingAccountId { get; set; }
        public Guid BasketItemId { get; set; }
    }
}
