using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.AuthServices;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Core.Mailing;
using Core.Security.Domain.Enums;
using Domain.Entities;
using MediatR;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator
{
    public class EnableEmailAuthenticatorCommandHandler : ICommandHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EnableEmailAuthenticatorCommandHandler(
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            IMailService mailService,
            AuthBusinessRules authBusinessRules,
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IUserRepository userRepository)
        {
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _unitOfWork = unitOfWork;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(
                   predicate: x => x.Id == request.UserId, asNoTracking: true
               );
            await _authBusinessRules.UserShouldExistsWhenSelected(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            user!.AuthenticatorType = AuthenticatorType.Email;
            await _userRepository.UpdateAsync(user);

            EmailAuthenticator emailAuthenticator = await _authService.CreateEmailAuthenticatorAsync(user);
            EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            var toEmailList = new List<MailboxAddress> { new(name: user.Email, user.Email) };

            await _mailService.SendMailAsync(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Verify Your Email",
                    TextBody =
                        $"Click on the link to verify your email: {request.VerifyEmailUrl}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
                }
            );
            await _unitOfWork.SaveChangesAsync();


            return Unit.Value;
        }
    }
}
