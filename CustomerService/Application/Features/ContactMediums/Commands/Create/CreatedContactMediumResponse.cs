using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Commands.Create
{
    public class CreatedContactMediumResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public ContactMediumType Type { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }
    }
}
