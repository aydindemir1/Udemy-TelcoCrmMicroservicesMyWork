using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferingPrices.Commands.Create
{
    public class CreateProductOfferingPriceCommandHandler
     : ICommandHandler<CreateProductOfferingPriceCommand, CreatedProductOfferingPriceResponse>
    {
        private readonly IProductOfferingPriceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductOfferingPriceCommandHandler(
            IProductOfferingPriceRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedProductOfferingPriceResponse> Handle(CreateProductOfferingPriceCommand request, CancellationToken cancellationToken)
        {
            ProductOfferingPrice mappedProductOfferingPrice = _mapper.Map<ProductOfferingPrice>(request);
            ProductOfferingPrice createdProductOfferingPrice = await _repository.AddAsync(mappedProductOfferingPrice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            CreatedProductOfferingPriceResponse response = _mapper.Map<CreatedProductOfferingPriceResponse>(createdProductOfferingPrice);
            return response;
        }
    }
}
