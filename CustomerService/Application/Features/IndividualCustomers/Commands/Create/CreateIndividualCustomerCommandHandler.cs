using Application.Features.IndividualCustomers.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.Create
{
    //Eski yapı 

    //public class CreateIndividualCustomerCommandHandler : IRequestHandler<CreateIndividualCustomerCommand, CreatedIndividualCustomerResponse>
    //{
    //    private readonly IIndividualCustomerRepository _individualCustomerRepository;
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public CreateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository, IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _individualCustomerRepository = individualCustomerRepository;
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<CreatedIndividualCustomerResponse> Handle(CreateIndividualCustomerCommand request, CancellationToken cancellationToken)
    //    {
    //        IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
    //        mappedIndividualCustomer.CustomerNumber = GenerateCustomerNumber();

    //        IndividualCustomer createdIndividualCustomer = await _individualCustomerRepository.AddAsync(mappedIndividualCustomer);
    //        await _unitOfWork.SaveChangesAsync(cancellationToken);

    //        CreatedIndividualCustomerResponse response = _mapper.Map<CreatedIndividualCustomerResponse>(createdIndividualCustomer);
    //        return response;
    //    }

    //    private string GenerateCustomerNumber()
    //    {
    //        var datePart = DateTimeOffset.UtcNow.ToString("yyyyMMdd");
    //        var random = Random.Shared.Next(1000, 9999);
    //        return $"CUST-{datePart}-{random}";
    //    }
    //}


    public class CreateIndividualCustomerCommandHandler : ICommandHandler<CreateIndividualCustomerCommand, CreatedIndividualCustomerResponse>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
        //private readonly IEventProcessor _eventProcessor;


        public CreateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateIndividualCustomerCommandHandler> logger, IndividualCustomerBusinessRules individualCustomerBusinessRules//, IEventProcessor eventProcessor
                                                                                                                                                                                                                )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
            //_eventProcessor = eventProcessor;
        }

        public async Task<CreatedIndividualCustomerResponse> Handle(CreateIndividualCustomerCommand request, CancellationToken cancellationToken)
        {

            await _individualCustomerBusinessRules.IndividualCustomerNationalityIdentityCannotBeDuplicatedWhenInserted(request.NationalIdentity);

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);

            IndividualCustomer createdIndividualCustomer = await _individualCustomerRepository.AddAsync(mappedIndividualCustomer);


            //CustomerCreatedIntegrationEvent customerCreatedEvent = new
            //(createdIndividualCustomer.Id, createdIndividualCustomer.CustomerNumber, createdIndividualCustomer.FirstName, createdIndividualCustomer.LastName, createdIndividualCustomer.NationalIdentity, createdIndividualCustomer.BirthDate);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Doğrudan eventi RabbitMQ tarafına gönder 
            //await _eventProcessor.PublishAsync(customerCreatedEvent,EventPublishingStrategy.Volatile, cancellationToken);

            //Outbox tablosuna ekle, daha sonra bu tabloyu dinleyen bir background service RabbitMQ'ya gönderecek
           // await _eventProcessor.PublishAsync(customerCreatedEvent, EventPublishingStrategy.Transactional, cancellationToken);

            CreatedIndividualCustomerResponse response = _mapper.Map<CreatedIndividualCustomerResponse>(createdIndividualCustomer);

            return response;



        }


    }
}
