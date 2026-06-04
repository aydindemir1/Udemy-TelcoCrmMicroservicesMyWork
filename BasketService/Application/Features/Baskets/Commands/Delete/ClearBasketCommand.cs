using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Commands.Delete
{
    public class ClearBasketCommand : IDeleteCommand//, IAuthenticationRequest
    {
        public Guid BillingAccountId { get; set; }
    }
}
