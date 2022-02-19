using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Appointments.Models;

internal class Practitioner : AggregateRoot
{
    public Practitioner(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    
    private readonly List<Appointment> _appointments = new List<Appointment>();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    public void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }
}