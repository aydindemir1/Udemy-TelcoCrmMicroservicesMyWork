using Application.Repositories;
using Application.Services.Districts;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Core.Abstractions.Events;
using Domain.Entities;
using Shared.Events.Addresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Addresses.Commands.Create
{
    public class CreateAddressCommandHandler : ICommandHandler<CreateAddressCommand, CreatedAddressResponse>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDistrictService _districtService;

        public CreateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IUnitOfWork unitOfWork, IEventProcessor eventProcessor , IDistrictService districtService
                                                                                                                       )
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           _eventProcessor = eventProcessor;
           _districtService = districtService;
        }

        public async Task<CreatedAddressResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            Address mappedAddress = _mapper.Map<Address>(request);
            Address createdAddress = await _addressRepository.AddAsync(mappedAddress);

            District district = await _districtService.GetDistrictWithCityAsync(createdAddress.DistrictId);

            AddressCreatedIntegrationEvent addressCreatedEvent = new(createdAddress.Id, createdAddress.CustomerId, district.Name, district.City.Name, createdAddress.Street, createdAddress.HouseNumber, createdAddress.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Doğrudan eventi RabbitMQ tarafına gönder 
            //await _eventProcessor.PublishAsync(addressCreatedEvent,EventPublishingStrategy.Volatile, cancellationToken);

            //Outbox tablosuna ekle, daha sonra bu tabloyu dinleyen bir background service RabbitMQ'ya gönderecek
            await _eventProcessor.PublishAsync(addressCreatedEvent, EventPublishingStrategy.Transactional, cancellationToken);
            CreatedAddressResponse response = _mapper.Map<CreatedAddressResponse>(createdAddress);
            return response;
        }
    }
}
