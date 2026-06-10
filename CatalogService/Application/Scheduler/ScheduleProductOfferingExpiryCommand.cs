using Core.Abstractions.Cqrs.Command;
using Core.Abstractions.Scheduler;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Scheduler
{
    public class ScheduleProductOfferingExpiryCommand : InternalCommand
    {
        public Guid ProductOfferingId { get; set; }

        public DateTime ValidTo { get; set; }

        public ProductOfferingStatus ExpireToStatus { get; set; } = ProductOfferingStatus.Retired;

        public ScheduleProductOfferingExpiryCommand(Guid productOfferingId, DateTime validTo, ProductOfferingStatus expireToStatus)
        {
            ProductOfferingId = productOfferingId;
            ValidTo = validTo;
            ExpireToStatus = expireToStatus;
        }
    }
    public class ScheduleProductOfferingExpiryCommandHandler : IInternalCommandHandler<ScheduleProductOfferingExpiryCommand>
    {
        private readonly IScheduler _scheduler;
        private readonly ILogger<ScheduleProductOfferingExpiryCommandHandler> _logger;

        public ScheduleProductOfferingExpiryCommandHandler(
            IScheduler scheduler,
            ILogger<ScheduleProductOfferingExpiryCommandHandler> logger)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        public async Task<Unit> Handle(ScheduleProductOfferingExpiryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Scheduling expiry for ProductOffering {ProductOfferingId} - ValidTo: {ValidTo}",
                request.ProductOfferingId, request.ValidTo);

            var jobId = $"expire_product_offering_{request.ProductOfferingId}";
            if (await _scheduler.ExistsAsync(jobId))
            {
                await _scheduler.RemoveSchedulerAsync(jobId);
                _logger.LogDebug(
                    "Cancelled existing expiry job for ProductOffering {Id}",
                    request.ProductOfferingId);
            }

            if (request.ValidTo.ToUniversalTime() <= DateTime.UtcNow)
            {
                _logger.LogWarning(
                    "ValidTo date {ValidTo} is in the past for ProductOffering {Id}. Scheduling immediate expiry.",
                    request.ValidTo, request.ProductOfferingId);

                var immediateExpireCommand = new ExpireSingleProductOfferingCommand(
                    request.ProductOfferingId,
                    request.ExpireToStatus,
                    request.ValidTo
                    );

                await _scheduler.ScheduleAsync(immediateExpireCommand, TimeSpan.FromSeconds(10));
                return Unit.Value;
            }

            var expireCommand = new ExpireSingleProductOfferingCommand(
                    request.ProductOfferingId,
                    request.ExpireToStatus,
                    request.ValidTo
                    );

            var scheduleAt = new DateTimeOffset(request.ValidTo);

            var localOffset = TimeZoneInfo.Local.GetUtcOffset(request.ValidTo);
            var finalScheduleAt = new DateTimeOffset(request.ValidTo, localOffset);

            await _scheduler.ScheduleAsync(
                expireCommand,
                finalScheduleAt,
                $"Expire ProductOffering: {request.ProductOfferingId}",
                cancellationToken);

            _logger.LogInformation(
                "Successfully scheduled expiry for ProductOffering {ProductOfferingId} at {ValidTo}",
                request.ProductOfferingId, request.ValidTo);

            return Unit.Value;
        }
    }
}
