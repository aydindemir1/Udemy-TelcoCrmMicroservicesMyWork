using Core.Abstractions.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Queries.GetList
{
    public class GetListIndividualCustomerQuery : IQuery<List<GetListIndividualCustomerResponse>>//, IAuthorizationRequest
    {
        public string[] Roles => ["Admin"];
    }
}
