using BusinessReporting.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessReporting.Api.Services;

public class ReportingService(ReportingDbContext db, IMemoryCache cache) : IReportingService
{
    public async Task<object> GetSummaryAsync(
        DateTime? from,
        DateTime? to,
        CancellationToken cancellationToken)
    {
        var start = from?.Date ?? DateTime.UtcNow.Date.AddDays(-30);
        var end = to?.Date.AddDays(1) ?? DateTime.UtcNow.Date.AddDays(1);
        var key = $"summary:{start:yyyyMMdd}:{end:yyyyMMdd}";

        if (cache.TryGetValue(key, out object? cached) && cached is not null)
            return cached;

        var result = await db.BusinessMetrics
            .Where(x => x.BusinessDate >= start && x.BusinessDate < end)
            .GroupBy(_ => 1)
            .Select(g => new
            {
                Revenue = g.Sum(x => x.Revenue),
                Transactions = g.Sum(x => x.Transactions),
                AverageConversionRate = g.Average(x => x.ConversionRate)
            })
            .SingleOrDefaultAsync(cancellationToken);

        var response = result ?? new { Revenue = 0m, Transactions = 0, AverageConversionRate = 0m };
        cache.Set(key, response, TimeSpan.FromMinutes(5));
        return response;
    }

    public async Task<object> GetTimelineAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken)
    {
        return await db.BusinessMetrics
            .Where(x => x.BusinessDate >= from.Date && x.BusinessDate < to.Date.AddDays(1))
            .GroupBy(x => x.BusinessDate.Date)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Date = g.Key,
                Revenue = g.Sum(x => x.Revenue),
                Transactions = g.Sum(x => x.Transactions),
                ConversionRate = g.Average(x => x.ConversionRate)
            })
            .ToListAsync(cancellationToken);
    }
}
