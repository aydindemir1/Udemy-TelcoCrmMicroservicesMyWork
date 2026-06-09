using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Queries.Internal.GetInternalByBillingAccount
{
    public class GetInternalBasketByBillingAccountQuery : IQuery<GetInternalBasketResponse>, IAuthenticationRequest
    {
        public Guid BillingAccountId { get; set; }
    }
}
