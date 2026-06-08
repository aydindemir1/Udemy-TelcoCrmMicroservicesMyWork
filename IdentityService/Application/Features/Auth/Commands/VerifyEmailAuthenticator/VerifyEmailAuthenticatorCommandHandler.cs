using Application.Features.Auth.Rules;
using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticator
{
    public class VerifyEmailAuthenticatorCommandHandler : ICommandHandler<VerifyEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailAuthenticatorCommandHandler(
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            AuthBusinessRules authBusinessRules
    ,
            IUnitOfWork unitOfWork)
        {
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(
                    predicate: e => e.ActivationKey == request.ActivationKey,
                    cancellationToken: cancellationToken
                );
            await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
            await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator!);

            emailAuthenticator!.ActivationKey = null;
            emailAuthenticator.IsVerified = true;
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
