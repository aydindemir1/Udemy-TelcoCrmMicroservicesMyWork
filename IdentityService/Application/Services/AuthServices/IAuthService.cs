using Core.Security.Jwt;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AccessToken> CreateAccessToken(User user);
        Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetOrCreateRefreshTokenAsync(User user, string ipAddress);
        Task<RefreshToken?> GetRefreshTokenByToken(string token);
        Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null, string? replacedByToken = null);
        Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason);
        Task<RefreshToken> RotateRefreshToken(User user, RefreshToken oldRefreshToken, string ipAddress);
        Task DeleteOldRefreshTokens(Guid userId);
        Task DeleteAllOldRefreshTokens(CancellationToken cancellationToken = default);
        Task SendAuthenticatorCodeAsync(User user, CancellationToken cancellationToken = default);
        Task VerifyAuthenticatorCodeAsync(User user, string code, CancellationToken cancellationToken = default);
        Task<EmailAuthenticator> CreateEmailAuthenticatorAsync(User user, CancellationToken cancellationToken = default);
    }
}
