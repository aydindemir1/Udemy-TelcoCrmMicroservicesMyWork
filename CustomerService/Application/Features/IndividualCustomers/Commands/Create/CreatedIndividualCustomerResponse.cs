using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.Create
{
    public class CreatedIndividualCustomerResponse
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentity { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
