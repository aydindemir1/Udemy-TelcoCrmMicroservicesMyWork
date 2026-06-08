using Application.Repositories;
using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Security.Domain.Enums;
using Core.Security.EmailAuthenticator;
using Core.Security.Jwt;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using MimeKit;
namespace Application.Services.AuthServices
{
    public class AuthManager : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthManager> _logger;
        private readonly IEmailAuthenticator _emailAuthenticator;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;

        public AuthManager(IRefreshTokenRepository refreshTokenRepository, ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork, ILogger<AuthManager> logger , IEmailAuthenticator emailAuthenticator, IMailService mailService, IEmailAuthenticatorRepository emailAuthenticatorRepository)

        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailAuthenticator = emailAuthenticator;
            _mailService = mailService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            return addedRefreshToken;
        }

        public Task<AccessToken> CreateAccessToken(User user)
        {
            AccessToken accessToken = _tokenGenerator.CreateToken(user);
            return Task.FromResult(accessToken);
        }

        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
        {
            Core.Security.Domain.Entities.RefreshToken baseRefreshToken = _tokenGenerator.CreateRefreshToken(user, ipAddress);
            Domain.Entities.RefreshToken refreshToken = new()
            {
                Id = baseRefreshToken.Id,
                CreatedByIp = ipAddress,
                CreatedDate = baseRefreshToken.CreatedDate,
                UpdatedDate = baseRefreshToken.UpdatedDate,
                DeletedDate = baseRefreshToken.DeletedDate,
                Expires = baseRefreshToken.Expires,
                ReasonRevoked = baseRefreshToken.ReasonRevoked,
                ReplacedByToken = baseRefreshToken.ReplacedByToken,
                RevokedByIp = baseRefreshToken.RevokedByIp,
                Revoked = baseRefreshToken.Revoked,
                Token = baseRefreshToken.Token,
                UserId = user.Id
            };
            return Task.FromResult(refreshToken);
        }


        public async Task<RefreshToken> GetOrCreateRefreshTokenAsync(User user, string ipAddress)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetAsync(rt => rt.UserId == user.Id && rt.Revoked == null && rt.Expires > DateTime.UtcNow);

            if (existingRefreshToken != null)
            {
                _logger.LogInformation("Reusing active session (Refresh Token ID:{RefreshTokenId}) for user {UserId}", existingRefreshToken.Id, user.Id);
                return existingRefreshToken;
            }

            _logger.LogInformation("No active session found for user {UserId}. Creating new Refresh Token.", user.Id);

            var newRefreshToken = await CreateRefreshToken(user, ipAddress);
            var addedRefreshToken = await AddRefreshToken(newRefreshToken);
            return addedRefreshToken;
        }

        public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
        {
            return await _refreshTokenRepository.GetAsync(rt => rt.Token == token && rt.Revoked == null && rt.Expires > DateTime.UtcNow);
        }

        public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
        {
            var descendant = await _refreshTokenRepository.GetAsync(rt => rt.Token == refreshToken.ReplacedByToken);

            if (descendant is null) return;

            if (descendant.Revoked == null && descendant.Expires > DateTime.UtcNow)
            {

                await RevokeRefreshToken(descendant, ipAddress, reason);

                await RevokeDescendantRefreshTokens(descendant, ipAddress, reason);
            }
        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null, string? replacedByToken = null)
        {

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReasonRevoked = reason;
            refreshToken.ReplacedByToken = replacedByToken;

            await _refreshTokenRepository.UpdateAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken oldRefreshToken, string ipAddress)
        {
            var newRefreshToken = await CreateRefreshToken(user, ipAddress);

            await RevokeRefreshToken(oldRefreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);

            return newRefreshToken;
        }

        public async Task DeleteOldRefreshTokens(Guid userId)
        {
            var oldTokens = await _refreshTokenRepository.GetListAsync(predicate: r => r.UserId == userId && (r.Expires < DateTime.UtcNow || r.Revoked != null));

            await _refreshTokenRepository.DeleteRangeAsync(oldTokens);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SendAuthenticatorCodeAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user.AuthenticatorType is AuthenticatorType.Email)
                await SendAuthenticatorCodeWithEmail(user);
        }

        public async Task VerifyAuthenticatorCodeAsync(User user, string code, CancellationToken cancellationToken = default)
        {
            if (user.AuthenticatorType is AuthenticatorType.Email)
                await VerifyAuthenticatorCodeWithEmail(user, code);
        }

        public async Task<EmailAuthenticator> CreateEmailAuthenticatorAsync(User user, CancellationToken cancellationToken = default)
        {
            EmailAuthenticator emailAuthenticator = new()
            {
                UserId = user.Id,
                ActivationKey = await _emailAuthenticator.CreateEmailActivationKey(),
                IsVerified = false
            };
            return emailAuthenticator;
        }

        private async Task SendAuthenticatorCodeWithEmail(User user)
        {
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(predicate: e =>
                e.UserId == user.Id, asNoTracking: true
            );
            if (emailAuthenticator is null)
                throw new NotFoundException("Email Authenticator not found.");
            if (!emailAuthenticator.IsVerified)
                throw new BusinessException("Email Authenticator must be is verified.");

            string authenticatorCode = await _emailAuthenticator.CreateEmailActivationCode();
            emailAuthenticator.ActivationKey = authenticatorCode;
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
            await _unitOfWork.SaveChangesAsync();

            var toEmailList = new List<MailboxAddress> { new(name: user.Email, address: user.Email) };

            await _mailService.SendMailAsync(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Authenticator Code",
                    TextBody = $"Enter your authenticator code: {authenticatorCode}"
                }
            );
        }


        private async Task VerifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
        {
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(predicate: e =>
                e.UserId == user.Id, asNoTracking: true
            );
            if (emailAuthenticator is null)
                throw new NotFoundException("Email Authenticator not found.");
            if (emailAuthenticator.ActivationKey != authenticatorCode)
                throw new BusinessException("Authenticator code is invalid.");
            emailAuthenticator.ActivationKey = null;
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAllOldRefreshTokens(CancellationToken cancellationToken = default)
        {
            var oldTokens = await _refreshTokenRepository.GetListAsync(predicate: r => (r.Expires < DateTime.UtcNow || r.Revoked != null));
            await _refreshTokenRepository.DeleteRangeAsync(oldTokens); // ayrıca burda permanent parametresi kullanarak kalıcı olarak silebiliriz.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
