using Core.Abstractions.Cqrs.Query;
using Core.ElasticSearch;
using Core.ElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Customers.Queries
{
    public class CustomerSearchQueryHandler : IQueryHandler<CustomerSearchQuery, List<ElasticSearchGetModel<CustomerSearchResponse>>>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public CustomerSearchQueryHandler(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task<List<ElasticSearchGetModel<CustomerSearchResponse>>> Handle(CustomerSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _elasticSearchService.SearchAsync<CustomerSearchResponse>(request.SearchParameters);
            return result;
        }
    }
}
