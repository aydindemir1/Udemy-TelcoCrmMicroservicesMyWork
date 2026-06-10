using Application.Scheduler;
using Core.Abstractions.Cqrs;
using Core.Abstractions.Events.Internal;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.EventHandlers
{
    public class ProductOfferingExpiryScheduledDomainEventHandler : IDomainEventHandler<ProductOfferingExpiryScheduledDomainEvent>
    {
        private readonly ICqrsProcessor _cqrsProcessor;

        public ProductOfferingExpiryScheduledDomainEventHandler(ICqrsProcessor cqrsProcessor)
        {
            _cqrsProcessor = cqrsProcessor;
        }

        public async Task Handle(ProductOfferingExpiryScheduledDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cqrsProcessor.SendAsync(new ScheduleProductOfferingExpiryCommand(notification.ProductOfferId, notification.ValidTo, Domain.Enums.ProductOfferingStatus.Retired), cancellationToken);
        }
    }
}
