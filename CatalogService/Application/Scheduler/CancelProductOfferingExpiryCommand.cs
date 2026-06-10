using Core.Abstractions.Cqrs.Command;
using Core.Abstractions.Scheduler;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Scheduler
{
    public class CancelProductOfferingExpiryCommand : InternalCommand
    {
        public Guid ProductOfferId { get; set; }

        public string CancellationReason { get; set; } = "ProductOffering updated or deleted";
    }

    public class CancelProductOfferingExpiryCommandHandler : IInternalCommandHandler<CancelProductOfferingExpiryCommand>
    {
        private readonly IScheduler _scheduler;
        private readonly ILogger<CancelProductOfferingExpiryCommandHandler> _logger;

        public CancelProductOfferingExpiryCommandHandler(IScheduler scheduler, ILogger<CancelProductOfferingExpiryCommandHandler> logger)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelProductOfferingExpiryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cancelling scheduled expiry for ProductOfferId: {ProductOfferId} due to: {CancellationReason}", request.ProductOfferId, request.CancellationReason);

            var jobId = $"expire_product_offer_{request.ProductOfferId}";

            if (await _scheduler.ExistsAsync(jobId))
            {
                await _scheduler.RemoveSchedulerAsync(jobId);
                _logger.LogInformation("Successfullt cancelled expiry job {JobId}", jobId);
            }
            else
            {
                _logger.LogDebug("No active expiry job found for ProductOfferId: {ProductOfferId} - nothing to cancel", request.ProductOfferId);
            }

            return Unit.Value;
        }
    }
}
