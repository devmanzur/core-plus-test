using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Reporting.Models;

public class AppointmentRecord
{
    public Guid UniqueId { get; set; }
    public PractitionerRecord Practitioner { get; set; }
    public string ClientName { get; set; }
    public string AppointmentType { get; set; }
    public int Duration { get; set; }
    public double Cost { get; set; }
    public double Revenue { get; set; }
    public DateTime Date { get; set; }

}