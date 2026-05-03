using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Commands.Create
{
    public class CreatedBillingAccountResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BillingAddressId { get; set; }
        public string Number { get; set; }//unique
        public string Name { get; set; }
        public string Description { get; set; }
        public BillingAccountType Type { get; set; } // Corporate, Individual, Prepaid
        public BillingAccountStatus Status { get; set; }
    }
}
