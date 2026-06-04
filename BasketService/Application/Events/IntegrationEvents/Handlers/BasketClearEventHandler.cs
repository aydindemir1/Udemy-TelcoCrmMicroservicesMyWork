using Application.Repositories;
using Core.Abstractions.Events.External;
using Domain.Entities;
using Shared.Events.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events.IntegrationEvents.Handlers
{
    public class BasketClearEventHandler : IIntegrationEventHandler<BasketClearIntegrationEvent>
    {
        private readonly IBasketRepository _basketRepository;

        public BasketClearEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(BasketClearIntegrationEvent @event, CancellationToken cancellationToken)
        {
            var basket = new Basket { BillingAccountId = Guid.Parse(@event.billingAccountId) };

            string key = basket.GetRedisKey();

            await _basketRepository.DeleteAsync(key);
        }
    }
}
