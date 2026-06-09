using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Queries.Internal.GetById
{
    public class GetInternalBillingAccountByIdQuery : IQuery<GetInternalBillingAccountResponse>, IAuthenticationRequest
    {
        public Guid Id { get; set; }
    }
}
