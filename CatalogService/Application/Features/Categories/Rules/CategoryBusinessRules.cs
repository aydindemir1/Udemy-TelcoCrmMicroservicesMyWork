using Application.Features.Categories.Constants;
using Application.Repositories;
using Core.Abstractions.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Rules
{
    public class CategoryBusinessRules : BaseBusinessRules
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CategoryNameCannotBeDuplicated(string name)
        {
            bool exists = await _categoryRepository.AnyAsync(c => c.Name == name);
            if (exists)
                throw new BusinessException(CategoryMessages.CategoryNameExists);
        }

    }
}
