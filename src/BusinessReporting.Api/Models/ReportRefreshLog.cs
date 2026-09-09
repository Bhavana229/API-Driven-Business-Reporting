namespace BusinessReporting.Api.Models;

public class ReportRefreshLog
{
    public long Id { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public string Status { get; set; } = "";
    public string Source { get; set; } = "";
    public string? ErrorMessage { get; set; }
}
