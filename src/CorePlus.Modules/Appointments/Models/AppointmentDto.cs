﻿namespace CorePlus.Modules.Appointments.Models;

public class AppointmentDto
{
    public Guid PractitionerId { get;  set; }
    public string ClientName { get;  set; }
    public string AppointmentType { get;  set; }
    public int Duration { get;  set; }
    public decimal Cost { get;  set; }
    public decimal Revenue { get;  set; }
    public DateTime Date { get; set; }
}