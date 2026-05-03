using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Queries.GetList
{
    public class GetListIndividualCustomerResponse
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentity { get; set; }
        public List<CustomerAddressResponse> Addresses { get; set; }
    }
}
