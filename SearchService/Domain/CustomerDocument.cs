using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class CustomerDocument
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalityIdentity { get; set; }
        public DateTimeOffset BirthDate { get; set; }

        public List<AddressDocument> Addresses { get; set; } = new();
        public List<ContactDocument> Contacts { get; set; } = new();
    }
}
