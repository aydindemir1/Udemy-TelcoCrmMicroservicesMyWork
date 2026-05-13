using Core.Abstractions.Events.External;
using Core.ElasticSearch;
using Domain;
using Shared.Events.Addresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events.IntegrationEvents.Handlers
{
    public class AddressCreatedIntegrationEventHandler : IIntegrationEventHandler<AddressCreatedIntegrationEvent>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public AddressCreatedIntegrationEventHandler(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task Handle(AddressCreatedIntegrationEvent @event, CancellationToken cancellationToken)
        {
            var customer = await _elasticSearchService.GetAsync<CustomerDocument>(id: @event.CustomerId.ToString(), indexName: "customers");
            var alreadyExists = customer.Addresses.Any(a => a.Id == @event.Id);
            if (alreadyExists)
                return;

            customer.Addresses.Add(new AddressDocument
            {
                Id = @event.Id,
                CustomerId = @event.CustomerId,
                Street = @event.Street,
                HouseName = @event.HouseName,
                Description = @event.Description,
                CityName = @event.CityName,
                DistrictName = @event.DistrictName,
            });

            await _elasticSearchService.UpdateAsync(new Core.ElasticSearch.Models.InsertOrUpdateModel(elasticId: customer.Id.ToString(), indexName: "customers", item: customer));
        }
    }
}
