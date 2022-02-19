using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Shared.Events;

public class AppointmentCreated : IDomainEvent
{
    public Guid PractitionerUniqueId { get; set; }
    public string PractitionerName { get; set; }
    public Guid AppointmentUniqueId { get; set; }
    public string ClientName { get; set; }
    public string AppointmentType { get; set; }
    public int Duration { get; set; }
    public double Cost { get; set; }
    public double Revenue { get; set; }
    public DateTime Date { get; set; }

    public AppointmentCreated()
    {

    }
}