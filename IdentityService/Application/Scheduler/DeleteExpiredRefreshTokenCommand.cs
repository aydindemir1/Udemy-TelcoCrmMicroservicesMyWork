using Application.Services.AuthServices;
using Core.Abstractions.Cqrs.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Scheduler
{
    public class DeleteExpiredRefreshTokenCommand : InternalCommand
    {
    }

    public class DeleteExpiredRefreshTokenCommandHandler : IInternalCommandHandler<DeleteExpiredRefreshTokenCommand>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<DeleteExpiredRefreshTokenCommandHandler> _logger;

        public DeleteExpiredRefreshTokenCommandHandler(ILogger<DeleteExpiredRefreshTokenCommandHandler> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<Unit> Handle(DeleteExpiredRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting expired refresh tokens...");

            await _authService.DeleteAllOldRefreshTokens(cancellationToken);

            _logger.LogInformation("Expired refresh tokens deleted successfully.");
            return Unit.Value;
        }
    }
}
