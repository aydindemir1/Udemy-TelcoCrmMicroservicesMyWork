using Application.Repositories;
using AutoMapper;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Commands.Create
{
    public class CreateBillingAccountCommandHandler : ICommandHandler<CreateBillingAccountCommand, CreatedBillingAccountResponse>
    {
        private readonly IBillingAccountRepository _billingAccountRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBillingAccountCommandHandler(IBillingAccountRepository billingAccountRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _billingAccountRepository = billingAccountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedBillingAccountResponse> Handle(CreateBillingAccountCommand request, CancellationToken cancellationToken)
        {
            BillingAccount mappedBillingAccount = _mapper.Map<BillingAccount>(request);
            mappedBillingAccount.Number = GenerateNumber();

            BillingAccount createdBillingAccount = await _billingAccountRepository.AddAsync(mappedBillingAccount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            CreatedBillingAccountResponse response = _mapper.Map<CreatedBillingAccountResponse>(createdBillingAccount);
            return response;
        }


        private string GenerateNumber()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Random.Shared.Next(1000, 9999);
            return $"BA-{datePart}-{randomPart}";
        }
    }
}
