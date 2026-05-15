using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristicValues.Commands.Create
{
    public class CreateProductSpecCharacteristicValueCommandHandler
    : ICommandHandler<CreateProductSpecCharacteristicValueCommand, CreatedProductSpecCharacteristicValueResponse>
    {
        private readonly IProductSpecCharacteristicValueRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductSpecCharacteristicValueCommandHandler(
            IProductSpecCharacteristicValueRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedProductSpecCharacteristicValueResponse> Handle(
            CreateProductSpecCharacteristicValueCommand request,
            CancellationToken cancellationToken)
        {
            ProductSpecCharacteristicValue mappedProductSpecCharacteristicValue = _mapper.Map<ProductSpecCharacteristicValue>(request);
            ProductSpecCharacteristicValue createdProductSpecCharacteristicValue = await _repository.AddAsync(mappedProductSpecCharacteristicValue);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreatedProductSpecCharacteristicValueResponse>(createdProductSpecCharacteristicValue);
        }
    }
}
