using Application.Features.Auth.Responses;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Core.Security.Hashing;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.Email);

            string passwordHash = _passwordHasher.HashPassword(request.Password);

            User user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Status = true,
                AuthenticatorType = Core.Security.Domain.Enums.AuthenticatorType.None
            };

            User createdUser = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            RegisteredResponse registeredResponse = new RegisteredResponse
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };
            return registeredResponse;
        }
    }
}
