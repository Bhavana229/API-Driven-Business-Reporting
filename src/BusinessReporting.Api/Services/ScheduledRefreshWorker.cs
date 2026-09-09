namespace BusinessReporting.Api.Services;

public class ScheduledRefreshWorker(
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<ScheduledRefreshWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var targetHour = configuration.GetValue<int>("Reporting:ScheduledRefreshHourUtc", 6);
            var now = DateTime.UtcNow;
            var next = now.Date.AddHours(targetHour);

            if (next <= now)
                next = next.AddDays(1);

            await Task.Delay(next - now, stoppingToken);

            try
            {
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IPowerBiRefreshService>();
                await service.TriggerRefreshAsync("Scheduled", stoppingToken);
                logger.LogInformation("Scheduled reporting refresh completed.");
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Scheduled reporting refresh failed.");
            }
        }
    }
}
