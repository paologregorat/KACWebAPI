using EdgeGateway.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EdgeGateway.Infrastructure.System
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly ListenQueueService _listenQueueService;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            ListenQueueService testService,
            ILogger<WindowsBackgroundService> logger) =>
            (_listenQueueService, _logger) = (testService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var tasks = new List<Task>();
                tasks.Add(ExecuteTestService(stoppingToken));
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);
            }
        }

        private async Task ExecuteTestService(CancellationToken stoppingToken)
        {   
            try
            {
                _listenQueueService.Execute();

                   
            }
            catch (Exception ex)
            {

            }
            
        }

    }
}