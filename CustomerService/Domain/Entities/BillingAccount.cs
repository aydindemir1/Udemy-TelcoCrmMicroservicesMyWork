using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BillingAccount : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public Guid BillingAddressId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BillingAccountType Type { get; set; }
        public BillingAccountStatus Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Address BillingAddress { get; set; }

        public BillingAccount()
        {

        }

        public BillingAccount(Guid id, Guid customerId, Guid billingAddressId, string number, string name, string description, BillingAccountType type, BillingAccountStatus status)
        {
            Id = id;
            CustomerId = customerId;
            BillingAddressId = billingAddressId;
            Number = number;
            Name = name;
            Description = description;
            Type = type;
            Status = status;
        }
    }
}
