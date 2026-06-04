using Application.Clients;
using Application.Repositories;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Commands.Create
{
    public class AddToBasketCommandHandler : ICommandHandler<AddToBasketCommand, Unit>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICustomerServiceClient _customerServiceClient;
        private readonly ICatalogServiceClient _catalogServiceClient;

        public AddToBasketCommandHandler(IBasketRepository basketRepository, ICustomerServiceClient customerServiceClient, ICatalogServiceClient catalogServiceClient)
        {
            _basketRepository = basketRepository;
            _customerServiceClient = customerServiceClient;
            _catalogServiceClient = catalogServiceClient;
        }

        public async Task<Unit> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
        {
            var billingAccountTask = _customerServiceClient.GetByBillingAccountId(request.BillingAccountId);
            var productOfferTask = _catalogServiceClient.GetByProductOfferId(request.ProductOfferId);

            await Task.WhenAll(billingAccountTask, productOfferTask);

            var billingAccount = await billingAccountTask;
            var productOffer = await productOfferTask;

            var tempBasket = new Basket { BillingAccountId = billingAccount.Id };
            string basketKey = tempBasket.GetRedisKey();

            var basket = await _basketRepository.GetAsync(basketKey) ?? new Basket(Guid.NewGuid(), billingAccount.Id);

            basket.AddOrUpdateItem(productOffer.Id, productOffer.Name, productOffer.PriceName, productOffer.UnitPrice, productOffer.PriceType, request.Quantity);
            await _basketRepository.SetAsync(basket, TimeSpan.FromHours(2));

            return Unit.Value;
        }
    }
}
