USE BusinessReportingDb;
GO

INSERT INTO dbo.BusinessMetrics
(BusinessDate, Region, Product, Revenue, Transactions, ConversionRate)
VALUES
('2026-08-01','North','Retail',125000.00,1250,0.0820),
('2026-08-01','South','Retail',98000.00,1010,0.0760),
('2026-08-01','West','Commercial',143000.00,890,0.0940),
('2026-08-02','North','Retail',131000.00,1320,0.0840),
('2026-08-02','South','Commercial',110000.00,920,0.0810),
('2026-08-02','West','Retail',151000.00,1420,0.0970),
('2026-08-03','North','Commercial',138000.00,870,0.0910),
('2026-08-03','South','Retail',104000.00,1080,0.0790),
('2026-08-03','West','Commercial',160000.00,940,0.1010);
GO
