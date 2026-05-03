using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.SoftDelete
{
    public class SoftDeleteIndividualCustomerCommandHandler : ICommandHandler<SoftDeleteIndividualCustomerCommand>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository, IUnitOfWork unitOfWork)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SoftDeleteIndividualCustomerCommand request, CancellationToken cancellationToken)
        {
            IndividualCustomer? individualCustomer = await _individualCustomerRepository.GetAsync(predicate: i => i.Id == request.Id, asNoTracking: true, cancellationToken: cancellationToken);
            await _individualCustomerRepository.DeleteAsync(individualCustomer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
