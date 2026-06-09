using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using Core.ElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Customers.Queries
{
    public class CustomerSearchQuery : IQuery<List<ElasticSearchGetModel<CustomerSearchResponse>>>, IAuthenticationRequest
    {
        public SearchParameters SearchParameters { get; set; }
    }
}
