using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Queries.GetList
{
    public class GetListProductSpecificationQuery : IQuery<List<GetListProductSpecificationResponse>>, IAuthenticationRequest
    {
        public Guid Id { get; set; }
    }
}
