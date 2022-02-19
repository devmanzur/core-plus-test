namespace CorePlus.Modules.Reporting.Models;

public class AppointmentCostRevenueSummaryDto
{
    public int AppointmentId { get; set; }
    public int PractitionerId { get; set; }
    public string PractitionerName { get; set; }
    public double Revenue { get; set; }
    public double Cost { get; set; }
    public double Profit => Revenue - Cost;
    public string Date { get; set; }
}