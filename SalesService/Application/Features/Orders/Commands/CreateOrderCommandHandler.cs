using Application.Clients;
using Application.Repositories;
using Core.Abstractions.Cqrs.Command;
using Core.Abstractions.Events;
using Domain.Entites;
using MediatR;
using Shared.Events.Baskets;
using Shared.Events.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Commands
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketServiceClient _basketServiceClient;
        private readonly ICustomerServiceClient _customerServiceClient;
        private readonly IEventProcessor _eventProcessor;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IBasketServiceClient basketServiceClient, ICustomerServiceClient customerServiceClient, IEventProcessor eventProcessor)
        {
            _orderRepository = orderRepository;
            _basketServiceClient = basketServiceClient;
            _customerServiceClient = customerServiceClient;
            _eventProcessor = eventProcessor;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var basketTask = _basketServiceClient.GetByBillingAccountId(request.BillingAccountId);
            var billingAccountTask = _customerServiceClient.GetByBillingAccountId(request.BillingAccountId);

            await Task.WhenAll(basketTask, billingAccountTask);

            var basket = await basketTask;
            var billingAccount = await billingAccountTask;


            var order = new Order(billingAccount.Id.ToString(), billingAccount.Number, billingAccount.Name, billingAccount.Description, billingAccount.Type, $"{billingAccount.AddressStreet},{billingAccount.AddressHouseNumber},{billingAccount.DistrictName},{billingAccount.CityName}", billingAccount.CustomerId.ToString(), billingAccount.CustomerName, DateTime.UtcNow, basket.TotalPrice, basket.Items.Select(x => new OrderItem
            {
                ProductOfferId = x.ProductOfferId.ToString(),
                ProductOfferName = x.ProductOfferName,
                PriceName = x.PriceName,
                UnitPrice = x.UnitPrice,
                PriceType = x.PriceType,
                Quantity = x.Quantity
            }).ToList());

            await _orderRepository.AddAsync(order);

            var basketClearEvent = new BasketClearIntegrationEvent(order.BillingAccountId);

            var orderCompletedEvent = new OrderCompletedIntegrationEvent(order.Number, order.BillingAccountId, order.BillingAccountNumber, order.BillingAccountName, order.BillingAccountDescription, order.BillingAccountType, order.BillingAddress, order.CustomerId, order.CustomerName, order.TotalPrice, order.Items.Select(x => new OrderItemDto
            {
                ProductOfferId = x.ProductOfferId,
                ProductOfferName = x.ProductOfferName,
                PriceName = x.PriceName,
                PriceType = x.PriceType,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList());


            await _eventProcessor.PublishAsync(new IEvent[] { basketClearEvent, orderCompletedEvent }, EventPublishingStrategy.Volatile);

            return Unit.Value;

        }


    }
}
