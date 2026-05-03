using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Queries.GetListPaginated
{
    public class GetListContactMediumResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public ContactMediumType Type { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }
        public string CustomerNumber { get; set; }
    }
}
