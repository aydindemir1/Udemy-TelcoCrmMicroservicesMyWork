using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Queries.Internal.GetInternalByBillingAccount
{
    public class GetInternalBasketByBillingAccountQueryHandler : IQueryHandler<GetInternalBasketByBillingAccountQuery, GetInternalBasketResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetInternalBasketByBillingAccountQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<GetInternalBasketResponse> Handle(GetInternalBasketByBillingAccountQuery request, CancellationToken cancellationToken)
        {
            var tempBasket = new Basket { BillingAccountId = request.BillingAccountId };
            var basketKey = tempBasket.GetRedisKey();
            var basket = await _basketRepository.GetAsync(basketKey, cancellationToken) ?? throw new BusinessException("Sepet bulunamadı");

            return new GetInternalBasketResponse
            {
                BillingAccountId = basket.BillingAccountId,
                TotalPrice = basket.TotalPrice,
                Items = basket.Items.Select(item => new GetInternalBasketItemResponse
                {
                    ProductOfferId = item.ProductOfferId,
                    ProductOfferName = item.ProductOfferName,
                    PriceType = item.PriceType,
                    PriceName = item.PriceName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }
    }
}
