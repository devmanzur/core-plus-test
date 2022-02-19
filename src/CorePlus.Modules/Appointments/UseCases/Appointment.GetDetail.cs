using CorePlus.Modules.Appointments.Models;
using Microsoft.EntityFrameworkCore;

namespace CorePlus.Modules.Appointments.UseCases;

public partial class AppointmentService
{
    
    public async Task<AppointmentDto?> GetAppointmentDetail(Guid appointmentId)
    {
        var detail = await _unitOfWork.Appointments
            .Include(x=>x.Practitioner)
            .AsNoTracking()
            .Select(x => new AppointmentDto()
            {
                Id = x.UniqueId,
                Cost = x.Cost,
                Date = x.Date,
                Duration = x.Duration,
                Revenue = x.Revenue,
                AppointmentType = x.AppointmentType,
                ClientName = x.ClientName,
                PractitionerId = x.Practitioner.UniqueId,
                PractitionerName = x.Practitioner.Name
            }).FirstOrDefaultAsync(x=>x.Id == appointmentId);

        return detail;
    }
}