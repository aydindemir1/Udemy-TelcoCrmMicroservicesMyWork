using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Address : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public short DistrictId { get; set; }
        public AddressType Type { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Description { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual District District { get; set; }

        public virtual ICollection<BillingAccount> BillingAccounts { get; set; }


        public Address()
        {
            BillingAccounts = new HashSet<BillingAccount>();
        }

        public Address(Guid id, Guid customerId, short districtId, AddressType type, string street, string houseNumber, string description)
        {
            Id = id;
            CustomerId = customerId;
            DistrictId = districtId;
            Type = type;
            Street = street;
            HouseNumber = houseNumber;
            Description = description;
        }
    }
}
