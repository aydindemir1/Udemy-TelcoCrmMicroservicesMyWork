using Application.Features.ProductOfferings.Constants;
using Application.Repositories;
using Application.Services.Categories;
using Application.Services.ProductSpecifications;
using Core.Abstractions.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Rules
{
    public class ProductOfferingRules : BaseBusinessRules
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly IProductOfferingRepository _offeringRepository;

        public ProductOfferingRules(ICategoryService categoryService, IProductSpecificationService productSpecificationService, IProductOfferingRepository offeringRepository)
        {
            _categoryService = categoryService;
            _productSpecificationService = productSpecificationService;
            _offeringRepository = offeringRepository;
        }

        public async Task CannotAddProductToInactiveCategory(Guid categoryId)
        {
            var category = await _categoryService.GetById(categoryId);
            if (category is null || !category.IsActive)
                throw new BusinessException(ProductOfferingMessages.CannotAddProductToInactive);
        }

        public async Task CannotCreateOfferingFromRetiredSpec(Guid productSpecificationId)
        {
            var spec = await _productSpecificationService.GetById(productSpecificationId);
            if (spec is null || spec.LifecycleStatus == LifecycleStatus.Retired)
                throw new BusinessException(ProductOfferingMessages.CannotCreateOfferFromRetired);
        }

        public async Task EnsureProductOfferingExists(Guid id)
        {
            var productOffer = await _offeringRepository.AnyAsync(x => x.Id == id);
            if (!productOffer)
                throw new BusinessException(ProductOfferingMessages.ProductOfferingNotFound);

        }
    }
}
