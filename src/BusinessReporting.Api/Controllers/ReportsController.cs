using BusinessReporting.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessReporting.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/reports")]
public class ReportsController(IReportingService reportingService) : ControllerBase
{
    [HttpGet("summary")]
    public async Task<IActionResult> Summary(
        DateTime? from,
        DateTime? to,
        CancellationToken cancellationToken)
        => Ok(await reportingService.GetSummaryAsync(from, to, cancellationToken));

    [HttpGet("timeline")]
    public async Task<IActionResult> Timeline(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken)
        => Ok(await reportingService.GetTimelineAsync(from, to, cancellationToken));
}
