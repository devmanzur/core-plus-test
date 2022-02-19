using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CorePlus.Modules.Appointments.UseCases;

public partial class AppointmentService
{
    public async Task<Result<AppointmentDto>> CreateAppointment(AppointmentDto request)
    {
        Maybe<Practitioner?> practitioner =
            await _unitOfWork.Practitioners.FirstOrDefaultAsync(x => x.UniqueId == request.PractitionerId);
        if (practitioner.HasNoValue)
        {
            return Result.Failure<AppointmentDto>("Practitioner not found!");
        }

        var appointment = new Appointment(request.Date, request.ClientName, request.AppointmentType, request.Duration,
            request.Cost,
            request.Revenue);
        practitioner.Value!.AddAppointment(appointment);

        await _unitOfWork.CommitAsync();
        return Result.Success(new AppointmentDto()
        {
            Id = appointment.UniqueId,
            PractitionerId = practitioner.Value!.UniqueId,
            PractitionerName = practitioner.Value!.Name,
            Cost = appointment.Cost,
            Duration = appointment.Duration,
            Revenue = appointment.Revenue,
            AppointmentType = appointment.AppointmentType,
            ClientName = appointment.ClientName,
        });
    }
}