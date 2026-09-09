# Interview Notes

## 60-second explanation

"I worked on an API-driven reporting solution where ASP.NET Core Web APIs acted as the business layer between SQL Server and Power BI. The goal was to give business stakeholders a faster way to access current reporting data without putting the entire reporting workload directly on the database.

I implemented JWT authentication, reporting APIs, SQL-based aggregations and caching for frequently requested summaries. I also supported both scheduled and on-demand refresh orchestration for Power BI. One thing I focused on was separating transactional data access from reporting workloads, so the API could return curated business metrics while Power BI handled visualization. The project gave me hands-on experience with API design, SQL Server, authentication, caching, background processing and BI integration."

## Questions you should be ready for

### Why use an API between SQL Server and Power BI?
It provides a controlled application/business layer, centralizes authorization and business rules, and can reduce repetitive direct access to the database.

### Why caching?
Summary/reporting requests often repeat the same aggregations. Short-lived caching reduces unnecessary database work and improves response time.

### How did JWT work?
The login endpoint issues a signed token containing identity/role claims. Protected endpoints validate the token, and role-based policies restrict administrative refresh operations.

### Scheduled vs on-demand refresh
Scheduled refresh is triggered by the background worker. On-demand refresh is exposed through an authorized POST endpoint.

### What would you improve for production?
Use ASP.NET Core Identity or an enterprise identity provider, Azure Key Vault/managed identity for secrets, distributed caching such as Redis for multiple API instances, structured logging/telemetry, retry policies, and a durable job queue for refresh orchestration.
