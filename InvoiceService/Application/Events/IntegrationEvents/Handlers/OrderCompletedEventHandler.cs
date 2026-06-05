using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Events.External;
using Domain.Entites;
using Shared.Events.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events.IntegrationEvents.Handlers
{
    public class OrderCompletedEventHandler : IIntegrationEventHandler<OrderCompletedIntegrationEvent>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderCompletedEventHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(OrderCompletedIntegrationEvent @event, CancellationToken cancellationToken)
        {
            var invoice = new Invoice(@event.OrderNumber, Guid.Parse(@event.BillingAccountId), @event.BillingAccountNumber, @event.BillingAccountName, @event.BillingAccountDescription, @event.BillingAccountType, @event.BillingAccountAddress, Guid.Parse(@event.CustomerId), @event.CustomerName, @event.TotalPrice, @event.OrderItems.Select(x => new InvoiceItem
            {
                ProductOfferingId = Guid.Parse(x.ProductOfferId),
                ProductOfferingName = x.ProductOfferName,
                PriceName = x.PriceName,
                UnitPrice = x.UnitPrice,
                PriceType = x.PriceType,
                Quantity = x.Quantity

            }).ToList());

            await _invoiceRepository.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
