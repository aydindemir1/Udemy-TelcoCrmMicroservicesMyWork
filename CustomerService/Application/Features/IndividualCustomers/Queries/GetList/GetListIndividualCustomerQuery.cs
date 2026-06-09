using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Queries.GetList
{
    public class GetListIndividualCustomerQuery : IQuery<List<GetListIndividualCustomerResponse>>, IAuthorizationRequest
    {
        public string[] Roles => ["Admin"];
    }
}
