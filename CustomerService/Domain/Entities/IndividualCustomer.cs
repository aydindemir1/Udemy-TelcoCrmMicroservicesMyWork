using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IndividualCustomer : Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentity { get; set; }
        private DateTimeOffset _birthDate;

        public DateTimeOffset BirthDate { get => _birthDate; set => _birthDate = value.ToUniversalTime(); }

        public IndividualCustomer() : base()
        {

        }

        public IndividualCustomer(string firstName, string lastName, string nationalIdentity) : base(Guid.NewGuid(), DateTime.UtcNow, null, null)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalIdentity = nationalIdentity;
        }
    }
}
