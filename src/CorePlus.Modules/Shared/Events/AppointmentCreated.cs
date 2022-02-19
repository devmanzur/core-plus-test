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
    public decimal Cost { get; set; }
    public decimal Revenue { get; set; }
    public DateTime Date { get; set; }

    public AppointmentCreated()
    {

    }
}