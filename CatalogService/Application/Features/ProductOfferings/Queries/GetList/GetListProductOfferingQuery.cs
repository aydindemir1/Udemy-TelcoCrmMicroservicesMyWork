using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Queries.GetList
{
    public class GetListProductOfferingQuery : IQuery<List<GetListProductOfferingResponse>>, IAuthenticationRequest
    {

    }
}
