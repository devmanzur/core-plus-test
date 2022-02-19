using CorePlus.Modules.Reporting.Interfaces;
using CorePlus.Modules.Reporting.Models;
using CorePlus.Modules.Shared.Events;
using MediatR;

namespace CorePlus.Modules.Reporting.UseCases;

public class OnAppointmentCreated : INotificationHandler<AppointmentCreated>
{
    private readonly IAppointmentReportService _appointmentReportService;

    public OnAppointmentCreated(IAppointmentReportService appointmentReportService)
    {
        _appointmentReportService = appointmentReportService;
    }
    
    public Task Handle(AppointmentCreated notification, CancellationToken cancellationToken)
    {
        return _appointmentReportService.CreateReportAsync(new AppointmentRecord()
        {
            Practitioner = new PractitionerRecord()
            {
                Id = notification.PractitionerUniqueId,
                Name = notification.PractitionerName
            },
            UniqueId = notification.AppointmentUniqueId,
            Cost = notification.Cost,
            Duration = notification.Duration,
            Revenue = notification.Revenue,
            Date = notification.Date,
            AppointmentType = notification.AppointmentType,
            ClientName = notification.ClientName,
            
        });
    }
}