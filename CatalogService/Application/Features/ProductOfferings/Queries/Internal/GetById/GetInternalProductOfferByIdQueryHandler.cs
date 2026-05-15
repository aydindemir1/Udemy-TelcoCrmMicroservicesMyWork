using Application.Features.ProductOfferings.Rules;
using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Queries.Internal.GetById
{
    public class GetInternalProductOfferByIdQueryHandler : IQueryHandler<GetInternalProductOfferByIdQuery, GetInternalProductOfferResponse>
    {
        private readonly IProductOfferingRepository _repository;
        private readonly ProductOfferingRules _rules;

        public GetInternalProductOfferByIdQueryHandler(IProductOfferingRepository repository, ProductOfferingRules rules)
        {
            _repository = repository;
            _rules = rules;
        }

        public async Task<GetInternalProductOfferResponse> Handle(GetInternalProductOfferByIdQuery request, CancellationToken cancellationToken)
        {
            await _rules.EnsureProductOfferingExists(request.Id);
            var response = await _repository.GetProjectedAsync(predicate: x => x.Id == request.Id, selector: x => new GetInternalProductOfferResponse
            {
                Id = x.Id,
                Name = x.Name,
                PriceName = x.ProductOfferingPrices.FirstOrDefault().Name,
                PriceType = x.ProductOfferingPrices.FirstOrDefault().PriceType.ToString(),
                UnitPrice = x.ProductOfferingPrices.FirstOrDefault().Amount

            });

            return response;
        }
    }
}
