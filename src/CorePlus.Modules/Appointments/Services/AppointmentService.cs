using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CorePlus.Modules.Appointments.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentDto>> CreateAppointment(AppointmentDto request)
    {
        Maybe<Practitioner?> practitioner =
            await _unitOfWork.Practitioners.FirstOrDefaultAsync(x => x.Id == request.PractitionerId);
        if (practitioner.HasNoValue)
        {
            return Result.Failure<AppointmentDto>("Practitioner not found!");
        }

        var appointment = new Appointment(request.ClientName, request.AppointmentType, request.Duration, request.Cost,
            request.Revenue);
        practitioner.Value!.AddAppointment(appointment);

        await _unitOfWork.CommitAsync();
        return Result.Success(new AppointmentDto()
        {
            Cost = appointment.Cost,
            Duration = appointment.Duration,
            Revenue = appointment.Revenue,
            AppointmentType = appointment.AppointmentType,
            ClientName = appointment.ClientName,
            PractitionerId = practitioner.Value!.Id
        });
    }
}