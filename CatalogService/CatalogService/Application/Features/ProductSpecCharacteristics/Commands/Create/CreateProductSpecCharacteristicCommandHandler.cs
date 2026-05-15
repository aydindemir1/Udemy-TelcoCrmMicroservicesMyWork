using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristics.Commands.Create
{
    public class CreateProductSpecCharacteristicCommandHandler
    : ICommandHandler<CreateProductSpecCharacteristicCommand, CreatedProductSpecCharacteristicResponse>
    {
        private readonly IProductSpecCharacteristicRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductSpecCharacteristicCommandHandler(
            IProductSpecCharacteristicRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedProductSpecCharacteristicResponse> Handle(
            CreateProductSpecCharacteristicCommand request,
            CancellationToken cancellationToken)
        {
            ProductSpecCharacteristic mappedProductSpecCharacteristic = _mapper.Map<ProductSpecCharacteristic>(request);
            ProductSpecCharacteristic createdProductSpecCharacteristic = await _repository.AddAsync(mappedProductSpecCharacteristic);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreatedProductSpecCharacteristicResponse>(createdProductSpecCharacteristic);
        }
    }
}
