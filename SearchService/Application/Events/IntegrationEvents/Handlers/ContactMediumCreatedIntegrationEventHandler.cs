using Core.Abstractions.Events.External;
using Core.ElasticSearch;
using Domain;
using Shared.Events.ContactMediums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events.IntegrationEvents.Handlers
{
    public class ContactMediumCreatedIntegrationEventHandler : IIntegrationEventHandler<ContactMediumCreatedIntegrationEvent>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public ContactMediumCreatedIntegrationEventHandler(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task Handle(ContactMediumCreatedIntegrationEvent @event, CancellationToken cancellationToken)
        {
            var customer = await _elasticSearchService.GetAsync<CustomerDocument>(id: @event.CustomerId.ToString(), indexName: "customers");
            var alreadyExists = customer.Contacts.Any(a => a.Id == @event.Id);
            if (alreadyExists)
                return;

            customer.Contacts.Add(new ContactDocument
            {
                Id = @event.Id,
                CustomerId = @event.CustomerId,
                Type = @event.Type,
                Value = @event.Value,
                IsPrimary = @event.IsPrimary
            });
            await _elasticSearchService.UpdateAsync(new Core.ElasticSearch.Models.InsertOrUpdateModel(elasticId: customer.Id.ToString(), indexName: "customers", item: customer));
        }
    }
}
