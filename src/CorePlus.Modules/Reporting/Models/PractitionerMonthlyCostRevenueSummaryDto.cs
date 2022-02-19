namespace CorePlus.Modules.Reporting.Models;

public class PractitionerMonthlyCostRevenueSummaryDto
{
    public Guid PractitionerId { get; set; }
    public string PractitionerName { get; set; }
    public double Revenue { get; set; }
    public double Cost { get; set; }
    public double Profit => Revenue - Cost;

    public DateTime Month { get; set; }
}