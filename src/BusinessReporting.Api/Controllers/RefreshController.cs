using BusinessReporting.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessReporting.Api.Controllers;

[ApiController]
[Authorize(Roles = "ReportAdmin")]
[Route("api/reports/refresh")]
public class RefreshController(IPowerBiRefreshService refreshService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        await refreshService.TriggerRefreshAsync("OnDemand", cancellationToken);
        return Accepted(new { status = "Refresh requested" });
    }
}
