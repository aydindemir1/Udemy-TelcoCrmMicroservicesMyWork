using Application.Scheduler;
using Core.Abstractions.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Scheduler
{
    public class SchedulerBootstrapperHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SchedulerBootstrapperHostedService> _logger;

        public SchedulerBootstrapperHostedService(IServiceProvider serviceProvider, ILogger<SchedulerBootstrapperHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        //public async Task StartAsync(CancellationToken cancellationToken)
        //{
        //    using var scope = _serviceProvider.CreateScope();
        //    var scheduler = scope.ServiceProvider.GetRequiredService<IScheduler>();

        //    _logger.LogInformation("Registering recurring Hangfire jobs...");

        //    await scheduler.ScheduleRecurringAsync(new DeleteExpiredRefreshTokenCommand(), name: "delete-expired-refresh-tokens", cronExpression: "04 23 * * *", description: "Deleted expired and revoked refresh tokens from the database", cancellationToken: cancellationToken);

        //    _logger.LogInformation("Recurring Hangfire jobs registered successfully.");
        //}

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<IScheduler>();

            _logger.LogInformation("Registering recurring Hangfire jobs...");

            var runAt = DateTime.Now.AddMinutes(2);
            var cron = $"{runAt.Minute} {runAt.Hour} * * *";

            await scheduler.ScheduleRecurringAsync(
                new DeleteExpiredRefreshTokenCommand(),
                name: "delete-expired-refresh-tokens",
                cronExpression: cron,
                description: "Deleted expired and revoked refresh tokens from the database",
                cancellationToken: cancellationToken);

            _logger.LogInformation("Recurring Hangfire jobs registered successfully.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
