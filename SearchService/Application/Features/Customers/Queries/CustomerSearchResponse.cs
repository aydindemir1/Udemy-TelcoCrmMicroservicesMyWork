using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Customers.Queries
{
    public class CustomerSearchResponse
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalityIdentity { get; set; }
        public DateTimeOffset BirthDate { get; set; }

        public List<AddressResponse> Addresses { get; set; } = new();
        public List<ContactResponse> Contacts { get; set; } = new();
    }
}
