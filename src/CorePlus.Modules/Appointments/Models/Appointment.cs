using CorePlus.Modules.Shared.Interfaces;

namespace CorePlus.Modules.Appointments.Models;

public class Appointment : BaseEntity
{
    public Appointment(DateTime date, string clientName, string appointmentType, int duration, double cost,
        double revenue)
    {
        Date = date;
        ClientName = clientName;
        AppointmentType = appointmentType;
        Duration = duration;
        Cost = cost;
        Revenue = revenue;
        UniqueId = Guid.NewGuid();
    }


    public long PractitionerId { get; private set; }
    public Practitioner Practitioner { get; private set; }
    public DateTime Date { get; private set; }
    public string ClientName { get; private set; }
    public string AppointmentType { get; private set; }
    public int Duration { get; private set; }
    public double Cost { get; private set; }
    public double Revenue { get; private set; }
    
}