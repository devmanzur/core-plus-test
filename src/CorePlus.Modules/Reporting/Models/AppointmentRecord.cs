using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Reporting.Models;

public class AppointmentRecord : BaseEntity
{
    public PractitionerRecord Practitioner { get; set; }
    public string ClientName { get; set; }
    public string AppointmentType { get; set; }
    public int Duration { get; set; }
    public decimal Cost { get; set; }
    public decimal Revenue { get; set; }
    public DateTime Date { get; set; }

}