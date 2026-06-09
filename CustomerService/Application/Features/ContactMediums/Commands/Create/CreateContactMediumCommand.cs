using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Commands.Create
{
    public class CreateContactMediumCommand : ICreateCommand<CreatedContactMediumResponse>, IAuthenticationRequest
    {
        public Guid CustomerId { get; set; }
        public ContactMediumType Type { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }
    }
}
