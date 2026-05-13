using Core.Abstractions.Events.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events.Addresses
{
    public record AddressCreatedIntegrationEvent(Guid Id, Guid CustomerId, string DistrictName, string CityName, string Street, string HouseName, string Description) : IntegrationEvent
    {
    }
}
