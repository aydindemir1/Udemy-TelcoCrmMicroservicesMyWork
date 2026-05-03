using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Commands.Create
{
    public class CreateContactMediumCommandHandler : ICommandHandler<CreateContactMediumCommand, CreatedContactMediumResponse>
    {
        private readonly IContactMediumRepository _contactMediumRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEventProcessor _eventProcessor;

        public CreateContactMediumCommandHandler(IContactMediumRepository contactMediumRepository, IMapper mapper, IUnitOfWork unitOfWork //, IEventProcessor eventProcessor
                                                                                                                                          )
        {
            _contactMediumRepository = contactMediumRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           // _eventProcessor = eventProcessor;
        }

        public async Task<CreatedContactMediumResponse> Handle(CreateContactMediumCommand request, CancellationToken cancellationToken)
        {
            ContactMedium mappedContactMedium = _mapper.Map<ContactMedium>(request);
            ContactMedium createdContactMedium = await _contactMediumRepository.AddAsync(mappedContactMedium);

            //ContactMediumCreatedIntegrationEvent contactMediumCreatedEvent = new(createdContactMedium.Id, createdContactMedium.CustomerId, createdContactMedium.Type.ToString(), createdContactMedium.Value, createdContactMedium.IsPrimary);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            //Doğrudan eventi RabbitMQ tarafına gönder 
            //await _eventProcessor.PublishAsync(contactMediumCreatedEvent, EventPublishingStrategy.Volatile, cancellationToken);

            //Outbox tablosuna ekle, daha sonra bu tabloyu dinleyen bir background service RabbitMQ'ya gönderecek
           // await _eventProcessor.PublishAsync(contactMediumCreatedEvent, EventPublishingStrategy.Transactional, cancellationToken);
            CreatedContactMediumResponse response = _mapper.Map<CreatedContactMediumResponse>(createdContactMedium);
            return response;
        }
    }
}
