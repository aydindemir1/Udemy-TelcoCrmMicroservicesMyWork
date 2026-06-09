using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Queries.GetByBillingAccountId
{
    public class GetBasketByBillingAccountQuery : IQuery<GetBasketResponse>, IAuthenticationRequest
    {
        public Guid BillingAccountId { get; set; }
    }
}
