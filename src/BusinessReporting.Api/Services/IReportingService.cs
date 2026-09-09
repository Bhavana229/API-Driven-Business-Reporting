namespace BusinessReporting.Api.Services;

public interface IReportingService
{
    Task<object> GetSummaryAsync(DateTime? from, DateTime? to, CancellationToken cancellationToken);
    Task<object> GetTimelineAsync(DateTime from, DateTime to, CancellationToken cancellationToken);
}
