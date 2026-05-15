using Application.Features.Categories.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryBusinessRules _businessRules;

        public CreateCategoryCommandHandler(
            ICategoryRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            CategoryBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _businessRules = businessRules;
        }

        public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.CategoryNameCannotBeDuplicated(request.Name);
            Category mappedCategory = _mapper.Map<Category>(request);
            Category createdCategory = await _repository.AddAsync(mappedCategory);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreatedCategoryResponse>(createdCategory);
        }
    }
}
