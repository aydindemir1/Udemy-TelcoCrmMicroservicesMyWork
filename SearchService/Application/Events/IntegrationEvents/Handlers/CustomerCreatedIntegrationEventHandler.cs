using Core.Abstractions.Events.External;
using Core.ElasticSearch;
using Domain;
using Microsoft.Extensions.Logging;
using Shared.Events.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events.IntegrationEvents.Handlers
{
    public class CustomerCreatedIntegrationEventHandler : IIntegrationEventHandler<CustomerCreatedIntegrationEvent>
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly ILogger<CustomerCreatedIntegrationEventHandler> _logger;
        public CustomerCreatedIntegrationEventHandler(IElasticSearchService elasticSearchService, ILogger<CustomerCreatedIntegrationEventHandler> logger)
        {
            _elasticSearchService = elasticSearchService;
            _logger = logger;
        }

        public async Task Handle(CustomerCreatedIntegrationEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CustomerCreatedIntegrationEvent received : {@event.CustomerNumber}");

            var document = new CustomerDocument
            {
                Id = @event.Id,
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                CustomerNumber = @event.CustomerNumber,
                NationalityIdentity = @event.NationalityIdentity,
                BirthDate = @event.BirthDate
            };

            var result = await _elasticSearchService.InsertAsync(new Core.ElasticSearch.Models.InsertOrUpdateModel(elasticId: document.Id.ToString(), indexName: "customers", item: document));

            if (result.Success)
            {
                _logger.LogInformation($"CustomerDocument inserted successfully for CustomerNumber : {@event.CustomerNumber}");
            }
            else
            {
                _logger.LogError($"Failed to insert CustomerDocument for CustomerNumber : {@event.CustomerNumber}");
            }
        }
    }
}
