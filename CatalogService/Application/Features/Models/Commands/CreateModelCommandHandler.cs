using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application.Features.Models.Commands
{
    public class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, CreatedModelResponse>
    {
        private readonly IModelRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateModelCommandHandler(IModelRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedModelResponse> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model createdModel = await _repository.AddAsync(mappedModel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            CreatedModelResponse response = _mapper.Map<CreatedModelResponse>(createdModel);
            return response;
        }
    }
}
