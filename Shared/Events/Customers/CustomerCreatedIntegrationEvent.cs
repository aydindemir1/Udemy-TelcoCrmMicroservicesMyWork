using Core.Abstractions.Events.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events.Customers
{
    public record CustomerCreatedIntegrationEvent(Guid Id, string CustomerNumber, string FirstName, string LastName, string NationalityIdentity, DateTimeOffset BirthDate) : IntegrationEvent
    {
    }
}
