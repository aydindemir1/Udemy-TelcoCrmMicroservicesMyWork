using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.ProductSpecifications
{
    public interface IProductSpecificationService
    {
        Task<ProductSpecification> GetById(Guid id);
    }
}
