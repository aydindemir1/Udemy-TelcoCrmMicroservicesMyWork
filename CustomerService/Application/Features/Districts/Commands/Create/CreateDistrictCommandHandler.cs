using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Districts.Commands.Create
{
    public class CreateDistrictCommandHandler : ICommandHandler<CreateDistrictCommand, CreatedDistrictResponse>
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDistrictCommandHandler(IDistrictRepository districtRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _districtRepository = districtRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedDistrictResponse> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            District mappedDistrict = _mapper.Map<District>(request);
            District createdDistrict = await _districtRepository.AddAsync(mappedDistrict);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            CreatedDistrictResponse response = _mapper.Map<CreatedDistrictResponse>(createdDistrict);
            return response;
        }
    }
}
