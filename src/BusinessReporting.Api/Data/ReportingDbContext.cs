using BusinessReporting.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessReporting.Api.Data;

public class ReportingDbContext(DbContextOptions<ReportingDbContext> options) : DbContext(options)
{
    public DbSet<BusinessMetric> BusinessMetrics => Set<BusinessMetric>();
    public DbSet<ReportRefreshLog> ReportRefreshLogs => Set<ReportRefreshLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessMetric>()
            .HasIndex(x => new { x.BusinessDate, x.Region });

        modelBuilder.Entity<ReportRefreshLog>()
            .HasIndex(x => x.StartedAtUtc);
    }
}
