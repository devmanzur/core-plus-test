namespace CorePlus.Modules.Reporting.Models;


public class MonthlyCostRevenueSummaryDto
{
    public long PractitionerId { get; set; }
    public double TotalRevenue { get; set; }
    public double TotalCost { get; set; }
    public string Month { get; set; }
}