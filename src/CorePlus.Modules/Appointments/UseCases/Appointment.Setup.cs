using CorePlus.Modules.Appointments.Interfaces;

namespace CorePlus.Modules.Appointments.UseCases;

public partial class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}