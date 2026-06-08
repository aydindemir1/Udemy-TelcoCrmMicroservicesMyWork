using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Responses
{
    public class RegisteredResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RegisteredResponse() { }

        public RegisteredResponse(Guid id, string email, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
