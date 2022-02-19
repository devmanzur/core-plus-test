using CorePlus.Modules.Shared.Events;
using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Appointments.Models;

public class Practitioner : AggregateRoot
{
    public Practitioner(string name)
    {
        Name = name;
        UniqueId = Guid.NewGuid();
    }

    public string Name { get; private set; }
    
    private readonly List<Appointment> _appointments = new List<Appointment>();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    public void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
        AddDomainEvent(new AppointmentCreated()
        {
            PractitionerUniqueId = this.UniqueId,
            PractitionerName = this.Name,
            AppointmentUniqueId = appointment.UniqueId,
            Cost = appointment.Cost,
            Duration = appointment.Duration,
            Revenue = appointment.Revenue,
            Date = appointment.Date,
            AppointmentType = appointment.AppointmentType,
            ClientName = appointment.ClientName
        });
    }
}