using Core.Abstractions.Events.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events.ContactMediums
{
    public record ContactMediumCreatedIntegrationEvent(Guid Id, Guid CustomerId, string Type, string Value, bool IsPrimary) : IntegrationEvent
    {
    }
}
