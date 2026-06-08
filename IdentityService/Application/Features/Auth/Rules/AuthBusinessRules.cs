using Application.Features.Auth.Constants;
using Application.Repositories;
using Application.Services.AuthServices;
using Core.Abstractions.ContextExecutions;
using Core.Abstractions.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Domain.Enums;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules : BaseBusinessRules
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthService _authService;

        public AuthBusinessRules(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IAuthService authService)

        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _authService = authService;
        }

        public Task EmailAuthenticatorShouldBeExists(Domain.Entities.EmailAuthenticator? emailAuthenticator)
        {
            if (emailAuthenticator is null)
                throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);
            return Task.CompletedTask;
        }

        public Task EmailAuthenticatorActivationKeyShouldBeExists(Domain.Entities.EmailAuthenticator emailAuthenticator)
        {
            if (emailAuthenticator.ActivationKey is null)
                throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);
            return Task.CompletedTask;
        }


        public Task UserShouldNotBeHaveAuthenticator(Domain.Entities.User user)
        {
            if (user.AuthenticatorType != AuthenticatorType.None)
                throw new BusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
                throw new BusinessException(AuthMessages.InvalidRefreshToken);
            return Task.CompletedTask;
        }

        public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken,
        string? tokenFromCookie)
        {
            if (refreshToken != null) return;

            if (!string.IsNullOrEmpty(tokenFromCookie))
            {
                var tokenInDb = await _refreshTokenRepository.GetAsync(rt => rt.Token ==
                 tokenFromCookie, asNoTracking: true, withDeleted: true);

                if (tokenInDb != null && tokenInDb.Revoked != null)
                {
                    await _authService.RevokeDescendantRefreshTokens(tokenInDb,
                     tokenInDb.CreatedByIp, "Attempted reuse of revoked token");
                }
            }

            throw new BusinessException(AuthMessages.RefreshDontExists);
        }


        public async Task UserEmailShouldBeNotExists(string email)
        {
            bool doesExists = await _userRepository.AnyAsync(u => u.Email == email);
            if (doesExists) throw new BusinessException(AuthMessages.UserEmailAlreadyExists);
        }

        public Task UserShouldExistsWhenSelected(User? user)
        {
            if (user is null)
                throw new BusinessException(AuthMessages.UserDontExists);
            return Task.CompletedTask;
        }


        public Task UserShouldNotBeLocked(User? user)
        {
            if (user is not null && user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
                throw new BusinessException(AuthMessages.UserShouldNotBeLocked);
            return Task.CompletedTask;
        }


        public async Task UserPasswordShouldMatch(User? user, string password)
        {
            if (_passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                if (user.AccessFailedCount > 0)
                {
                    user.AccessFailedCount = 0;
                    user.LockoutEnd = null;
                    await _userRepository.UpdateAsync(user);
                    await _unitOfWork.SaveChangesAsync();

                }
                return;
            }

            user.AccessFailedCount++;

            int maxFailedAccessAttempts = _configuration.GetValue<int>("AuthSettings:MaxFailedAccessAttempts", 5);

            bool shouldLockAccount = user.AccessFailedCount >= maxFailedAccessAttempts;

            if (shouldLockAccount)
            {
                int lockoutDurationInMinutes = _configuration.GetValue<int>("AuthSettings:LockoutDurationInMinutes", 15);
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(lockoutDurationInMinutes);
                user.IsLockedOut = true;
            }

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            if (shouldLockAccount)
            {
                throw new BusinessException(AuthMessages.AccountLocked);
            }
            else
            {
                throw new BusinessException(AuthMessages.PasswordDontMatch);
            }
        }
    }
}
