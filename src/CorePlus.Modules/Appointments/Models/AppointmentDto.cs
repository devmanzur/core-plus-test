namespace CorePlus.Modules.Appointments.Models;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public Guid PractitionerId { get;  set; }
    public string PractitionerName { get; set; }
    public string ClientName { get;  set; }
    public string AppointmentType { get;  set; }
    public int Duration { get;  set; }
    public double Cost { get;  set; }
    public double Revenue { get;  set; }
    public DateTime Date { get; set; }
}

public class AppointmentCreateDto
{
    public int PractitionerId { get;  set; }
    public string ClientName { get;  set; }
    public string AppointmentType { get;  set; }
    public int Duration { get;  set; }
    public double Cost { get;  set; }
    public double Revenue { get;  set; }
    public DateTime Date { get; set; }
}