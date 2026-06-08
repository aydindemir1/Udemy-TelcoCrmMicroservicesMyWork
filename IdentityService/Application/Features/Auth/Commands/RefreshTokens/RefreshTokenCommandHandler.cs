using Application.Features.Auth.Responses;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.AuthServices;
using Core.Abstractions.Cqrs.Command;
using Core.Security.Jwt;
using Domain.Entities;

namespace Application.Features.Auth.Commands.RefreshTokens
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RefreshedTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;

        public RefreshTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IUserRepository userRepository)
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
        }

        public async Task<RefreshedTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.RefreshToken);

            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken, request.RefreshToken);

            if (refreshToken!.Revoked != null)
            {
                await _authService.RevokeDescendantRefreshTokens(
                    refreshToken,
                    request.IpAddress,
                    reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}"
                );
            }
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            User? user = await _userRepository.GetAsync(u => u.Id == refreshToken.UserId);

            await _authBusinessRules.UserShouldExistsWhenSelected(user);

            RefreshToken newRefreshToken = await _authService.RotateRefreshToken(
                user: user!,
                refreshToken,
                request.IpAddress
            );

            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);

            await _authService.DeleteOldRefreshTokens(refreshToken.UserId);
            AccessToken createdAccessToken = await _authService.CreateAccessToken(user!);

            RefreshedTokenResponse refreshedTokensResponse =
                new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };

            return refreshedTokensResponse;
        }
    }
}
