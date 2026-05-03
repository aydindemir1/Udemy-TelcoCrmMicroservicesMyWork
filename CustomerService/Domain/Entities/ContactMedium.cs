using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ContactMedium : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public ContactMediumType Type { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Customer Customer { get; set; }

        public ContactMedium()
        {

        }

        public ContactMedium(Guid id, Guid customerId, ContactMediumType type, string value, bool isPrimary)
        {
            Id = id;
            CustomerId = customerId;
            Type = type;
            Value = value;
            IsPrimary = isPrimary;
        }
    }
}
