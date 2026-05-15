using Application.Features.ProductOfferings.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Commands.Create
{
    public class CreateProductOfferingCommandHandler : ICommandHandler<CreateProductOfferingCommand, CreatedProductOfferingResponse>
    {
        private readonly IProductOfferingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductOfferingRules _rules;

        public CreateProductOfferingCommandHandler(
            IProductOfferingRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ProductOfferingRules rules,
            IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _rules = rules;
        }

        public async Task<CreatedProductOfferingResponse> Handle(CreateProductOfferingCommand request, CancellationToken cancellationToken)
        {

            return await _unitOfWork.ExecuteTransactionalAsync(async () =>
            {
                await _rules.CannotAddProductToInactiveCategory(request.CategoryId);
                await _rules.CannotCreateOfferingFromRetiredSpec(request.ProductSpecificationId);

                var productOffering = ProductOffering.Create(request.CategoryId, request.ProductSpecificationId, request.Name, request.Description, request.ValidFrom, request.ValidTo, request.Status);

                var createdProductOffering = await _repository.AddAsync(productOffering);

                foreach (var price in request.Prices)
                {
                    createdProductOffering.AddPrice(price.Name, price.Amount, price.Currency, price.PriceType);
                }

                return _mapper.Map<CreatedProductOfferingResponse>(createdProductOffering);
            });

        }
    }
}
