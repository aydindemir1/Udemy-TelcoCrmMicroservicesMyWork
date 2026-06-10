using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Scheduler
{
    public class ExpireSingleProductOfferingCommand : InternalCommand
    {
        public Guid ProductOfferId { get; set; }

        public ProductOfferingStatus NewStatus { get; set; } = ProductOfferingStatus.Retired;

        public DateTime ExpectedValidTo { get; set; }

        public ExpireSingleProductOfferingCommand(Guid productOfferId, ProductOfferingStatus newStatus, DateTime expectedValidTo)
        {
            ProductOfferId = productOfferId;
            NewStatus = newStatus;
            ExpectedValidTo = expectedValidTo;
        }
    }


    public class ExpireSingleProductOfferingCommandHandler : IInternalCommandHandler<ExpireSingleProductOfferingCommand>
    {
        private readonly IProductOfferingRepository _productOfferingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExpireSingleProductOfferingCommandHandler> _logger;

        public ExpireSingleProductOfferingCommandHandler(IProductOfferingRepository productOfferingRepository, IUnitOfWork unitOfWork, ILogger<ExpireSingleProductOfferingCommandHandler> logger)
        {
            _productOfferingRepository = productOfferingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(ExpireSingleProductOfferingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing scheduled expiry for ProductOffering {ProductOfferId} - Expected ValidTo : {ExpectedValidTo}", request.ProductOfferId, request.ExpectedValidTo);

            ProductOffering? productOffering = await _productOfferingRepository.GetAsync(predicate: po => po.Id == request.ProductOfferId, cancellationToken: cancellationToken);

            if (productOffering == null)
            {
                _logger.LogWarning("ProductOffering with Id {ProductOfferId} not found. Skipping expiry.", request.ProductOfferId);
                return Unit.Value;
            }

            if (productOffering.Status != ProductOfferingStatus.Active)
            {
                _logger.LogWarning("ProductOffering with Id {ProductOfferId} is not active (current status: {CurrentStatus}). Skipping expiry.", request.ProductOfferId, productOffering.Status);
                return Unit.Value;
            }

            if (productOffering.ValidTo != request.ExpectedValidTo)
            {
                _logger.LogWarning("ProductOffering {Id} ValidTo has changed from {Expected} to {Actual} - skipping scheduled expiry", request.ProductOfferId, request.ExpectedValidTo, productOffering.ValidTo);
                return Unit.Value;
            }

            productOffering.Status = request.NewStatus;
            await _productOfferingRepository.UpdateAsync(productOffering);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully expired ProductOffering {ProductOfferId} - new status: {NewStatus}", request.ProductOfferId, request.NewStatus);

            return Unit.Value;
        }
    }
}
