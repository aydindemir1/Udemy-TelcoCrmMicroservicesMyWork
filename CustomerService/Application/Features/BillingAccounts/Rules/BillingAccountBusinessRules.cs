using Application.Features.BillingAccounts.Constants;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Rules
{
    public class BillingAccountBusinessRules // : BaseBusinessRules
    {
        private readonly IBillingAccountRepository _billingAccountRepository;

        public BillingAccountBusinessRules(IBillingAccountRepository billingAccountRepository)
        {
            _billingAccountRepository = billingAccountRepository;
        }

        public async Task EnsureBillingAccountExists(Guid id)
        {
            var billingAccount = await _billingAccountRepository.AnyAsync(x => x.Id == id);
          //  if (!billingAccount)
                //throw new BusinessException(BillingAccountMessages.BillingAccountNotFound);
        }
    }
}
