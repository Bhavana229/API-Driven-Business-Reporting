IF DB_ID('BusinessReportingDb') IS NULL
    CREATE DATABASE BusinessReportingDb;
GO

USE BusinessReportingDb;
GO

IF OBJECT_ID('dbo.BusinessMetrics', 'U') IS NOT NULL DROP TABLE dbo.BusinessMetrics;
IF OBJECT_ID('dbo.ReportRefreshLogs', 'U') IS NOT NULL DROP TABLE dbo.ReportRefreshLogs;
GO

CREATE TABLE dbo.BusinessMetrics
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    BusinessDate DATE NOT NULL,
    Region NVARCHAR(100) NOT NULL,
    Product NVARCHAR(100) NOT NULL,
    Revenue DECIMAL(18,2) NOT NULL,
    Transactions INT NOT NULL,
    ConversionRate DECIMAL(8,4) NOT NULL
);

CREATE INDEX IX_BusinessMetrics_Date_Region
ON dbo.BusinessMetrics(BusinessDate, Region);

CREATE TABLE dbo.ReportRefreshLogs
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    StartedAtUtc DATETIME2 NOT NULL,
    CompletedAtUtc DATETIME2 NULL,
    Status NVARCHAR(30) NOT NULL,
    Source NVARCHAR(30) NOT NULL,
    ErrorMessage NVARCHAR(2000) NULL
);

CREATE VIEW dbo.vw_DailyBusinessReporting
AS
SELECT
    BusinessDate,
    SUM(Revenue) AS Revenue,
    SUM(Transactions) AS Transactions,
    AVG(ConversionRate) AS AverageConversionRate
FROM dbo.BusinessMetrics
GROUP BY BusinessDate;
GO
