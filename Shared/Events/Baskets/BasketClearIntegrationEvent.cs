using Core.Abstractions.Events.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events.Baskets
{
    public record BasketClearIntegrationEvent(string billingAccountId) : IntegrationEvent
    {
    }
}
