namespace CorePlus.Modules.Reporting.Models;

public class AppointmentDetailDto : AppointmentCostRevenueSummaryDto
{
    public string ClientName { get;  set; }
    public string AppointmentType { get;  set; }
    public int Duration { get;  set; }
}