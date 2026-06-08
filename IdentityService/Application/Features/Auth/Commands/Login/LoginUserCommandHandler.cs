using Application.Features.Auth.Responses;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.AuthServices;
using Core.Abstractions.Cqrs.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoggedResponse>
    {
        public readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository, AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
            await _authBusinessRules.UserShouldExistsWhenSelected(user);
            await _authBusinessRules.UserShouldNotBeLocked(user);
            await _authBusinessRules.UserPasswordShouldMatch(user!, request.UserForLoginDto.Password);

            LoggedResponse response = new();

            if (user!.AuthenticatorType is not Core.Security.Domain.Enums.AuthenticatorType.None)
            {
                if (request.UserForLoginDto.AuthenticatorCode is null)
                {
                    await _authService.SendAuthenticatorCodeAsync(user);
                    // Sadece gerekli alanları dönüyoruz, token'ı bilerek boş bırakıyoruz.
                    return new LoggedResponse
                    {
                        AuthenticatorType = user.AuthenticatorType,
                        Email = user.Email // Kullanıcıya hangi maile kod gittiğini göstermek için.
                    };
                }

                await _authService.VerifyAuthenticatorCodeAsync(user, request.UserForLoginDto.AuthenticatorCode);
            }

            var createdAccessToken = await _authService.CreateAccessToken(user);
            var refreshTokenToUse = await _authService.GetOrCreateRefreshTokenAsync(user, request.IpAddress);

            response.UserId = user.Id;
            response.AccessToken = createdAccessToken;
            response.Email = user.Email;
            response.RefreshToken = refreshTokenToUse;
            return response;
        }
    }
}
