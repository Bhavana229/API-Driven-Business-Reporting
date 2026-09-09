namespace BusinessReporting.Api.Models;

public class BusinessMetric
{
    public long Id { get; set; }
    public DateTime BusinessDate { get; set; }
    public string Region { get; set; } = "";
    public string Product { get; set; } = "";
    public decimal Revenue { get; set; }
    public int Transactions { get; set; }
    public decimal ConversionRate { get; set; }
}
