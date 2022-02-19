using CorePlus.Modules.Appointments.Models;
using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Appointments.Events;

public class OnAppointmentCreated : IDomainEvent
{
    public Practitioner Practitioner { get; }
    public Appointment Appointment { get; }

    public OnAppointmentCreated(Practitioner practitioner, Appointment appointment)
    {
        Practitioner = practitioner;
        Appointment = appointment;
    }
}