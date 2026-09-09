namespace BusinessReporting.Api.Services;

public interface IPowerBiRefreshService
{
    Task TriggerRefreshAsync(string source, CancellationToken cancellationToken);
}
