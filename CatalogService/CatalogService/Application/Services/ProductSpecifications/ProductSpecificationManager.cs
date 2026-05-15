using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.ProductSpecifications
{
    public class ProductSpecificationManager : IProductSpecificationService
    {

        private readonly IProductSpecificationRepository _repository;

        public ProductSpecificationManager(IProductSpecificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductSpecification> GetById(Guid id)
        {
            return await _repository.GetAsync(predicate: x => x.Id == id, asNoTracking: true);
        }
    }
}
