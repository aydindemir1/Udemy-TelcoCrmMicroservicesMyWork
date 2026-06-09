using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Queries.Internal.GetById
{
    public class GetInternalProductOfferByIdQuery : IQuery<GetInternalProductOfferResponse>, IAuthenticationRequest
    {
        public Guid Id { get; set; }
    }
}
