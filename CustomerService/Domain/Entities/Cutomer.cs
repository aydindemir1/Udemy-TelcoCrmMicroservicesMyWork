using Core.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Entities
{
    public class Customer : BaseEntity<Guid>
    {
        public string CustomerNumber { get;  set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ContactMedium> ContactMediums { get; set; }
        public virtual ICollection<BillingAccount> BillingAccounts { get; set; }

        public Customer()
        {
            Addresses = new HashSet<Address>();
            ContactMediums = new HashSet<ContactMedium>();
            BillingAccounts = new HashSet<BillingAccount>();
        }

        public Customer(Guid id, DateTime createdDate, DateTime? updatedDate, DateTime? deletedDate)
        {
            Id = id;
            CustomerNumber = GenerateCustomerNumber();
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            DeletedDate = deletedDate;
        }

        private string GenerateCustomerNumber()
        {
            var datePart = DateTimeOffset.UtcNow.ToString("yyyyMMdd");
            var random = Random.Shared.Next(1000, 9999);
            return $"CUST-{datePart}-{random}";
        }
    }
}
