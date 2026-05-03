using Core.Abstractions.Cqrs.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Commands.Create
{
    //Eski yapı 

    //public class CreateIndividualCustomerCommand : IRequest<CreatedIndividualCustomerResponse>
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string NationalIdentity { get; set; }
    //    public DateTimeOffset BirthDate { get; set; }
    //}

    // Yeni yapı
    public class CreateIndividualCustomerCommand : ICreateCommand<CreatedIndividualCustomerResponse> //, IAuthenticationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentity { get; set; }
        public DateTimeOffset BirthDate { get; set; }
    }
}
