namespace CorePlus.Modules.Reporting.Models;


public class MonthlyCostRevenueBucketDto
{
    public int PractitionerId { get; set; }
    public string PractitionerName { get; set; }
    public double TotalRevenue { get; set; }
    public double TotalCost { get; set; }
    public string Date { get; set; }
}

public class MonthlyCostRevenueSummaryDto
{
    public long PractitionerId { get; set; }
    public string PractitionerName { get; set; }
    public double TotalRevenue { get; set; }
    public double TotalCost { get; set; }
    public int GroupingMonth { get; set; }
    public int GroupingYear { get; set; }
    // public double Profit => TotalRevenue - TotalCost;
    //
    // public DateTime Month => new DateTime(GroupingYear, GroupingMonth, 1);
}