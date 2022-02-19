using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Appointments.Models;

internal class Appointment : BaseEntity
{
    public int PractitionerId { get; private set; }
    public Practitioner Practitioner { get; private set; }
    public string ClientName { get; private set; }
    public string AppointmentType { get; private set; }
    public int Duration { get; private set; }
    public decimal Cost { get; private set; }
    public decimal Revenue { get; private set; }
    
}