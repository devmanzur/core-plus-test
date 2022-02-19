using CorePlus.Modules.Appointments.Models;
using CSharpFunctionalExtensions;

namespace CorePlus.Modules.Appointments.Interfaces;

public interface IAppointmentService
{
    Task<Result<AppointmentDto>> CreateAppointment(AppointmentDto request);
    Task<AppointmentDto?> GetAppointmentDetail(Guid appointmentId);

}