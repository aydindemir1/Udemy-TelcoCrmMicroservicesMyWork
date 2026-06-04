using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Queries.GetByBillingAccountId
{
    public class GetBasketByBillingAccountQueryHandler : IQueryHandler<GetBasketByBillingAccountQuery, GetBasketResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByBillingAccountQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<GetBasketResponse> Handle(GetBasketByBillingAccountQuery request, CancellationToken cancellationToken)
        {
            var tempBasket = new Basket { BillingAccountId = request.BillingAccountId };
            var basketKey = tempBasket.GetRedisKey();
            var basket = await _basketRepository.GetAsync(basketKey, cancellationToken) ?? throw new BusinessException("Sepet bulunamadı");

            return new GetBasketResponse
            {
                BillingAccountId = basket.BillingAccountId,
                TotalPrice = basket.TotalPrice,
                Items = basket.Items.Select(item => new GetBasketItemResponse
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
