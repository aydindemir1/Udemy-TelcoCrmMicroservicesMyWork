using Core.Abstractions.Events.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public record ProductOfferingExpiryScheduledDomainEvent : DomainEvent
    {
        public Guid ProductOfferId { get; set; }

        public DateTime ValidTo { get; set; }

        public ProductOfferingExpiryScheduledDomainEvent(Guid productOfferId, DateTime validTo)
        {
            ProductOfferId = productOfferId;
            ValidTo = validTo;
        }
    }
}
