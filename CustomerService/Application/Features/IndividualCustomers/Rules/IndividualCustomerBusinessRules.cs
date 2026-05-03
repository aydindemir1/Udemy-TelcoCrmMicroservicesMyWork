using Application.Features.IndividualCustomers.Constants;
using Application.Repositories;
using Core.Abstractions.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Rules
{
    public class IndividualCustomerBusinessRules : BaseBusinessRules
    {
        private readonly IIndividualCustomerRepository _repository;

        public IndividualCustomerBusinessRules(IIndividualCustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task IndividualCustomerNationalityIdentityCannotBeDuplicatedWhenInserted(string nationalityId)
        {
            var exists = await _repository.AnyAsync(predicate: x => x.NationalIdentity == nationalityId);
            if (exists)
               throw new BusinessException(IndividualCustomerMessages.NationalityIdExist);
        }
    }
}
