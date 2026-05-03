using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Cities.Commands.Create
{
    public class CreateCityCommandHandler : ICommandHandler<CreateCityCommand, CreatedCityResponse>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedCityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City mappedCity = _mapper.Map<City>(request);
            City createdCity = await _cityRepository.AddAsync(mappedCity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            CreatedCityResponse response = _mapper.Map<CreatedCityResponse>(createdCity);
            return response;
        }
    }
}
