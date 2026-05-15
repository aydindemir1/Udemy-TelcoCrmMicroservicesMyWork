using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Commands
{
    public class CreateProductSpecificationCommandHandler
    : ICommandHandler<CreateProductSpecificationCommand, CreatedProductSpecificationResponse>
    {
        private readonly IProductSpecificationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductSpecificationCommandHandler(
            IProductSpecificationRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedProductSpecificationResponse> Handle(CreateProductSpecificationCommand request, CancellationToken cancellationToken)
        {

            ProductSpecification mappedProductSpecification = _mapper.Map<ProductSpecification>(request);
            ProductSpecification createdProductSpecification = await _repository.AddAsync(mappedProductSpecification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreatedProductSpecificationResponse>(createdProductSpecification);
        }
    }
}
