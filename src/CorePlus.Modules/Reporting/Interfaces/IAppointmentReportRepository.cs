using CorePlus.Modules.Reporting.Models;

namespace CorePlus.Modules.Reporting.Interfaces;

public interface IAppointmentReportRepository
{
    Task CreateReportAsync(AppointmentRecord appointment);

    Task<List<PractitionerMonthlyCostRevenueSummaryDto>> GetMonthlyProfitSummary(List<Guid> practitionerIds, DateTime startDate,
        DateTime endDate);

    Task<List<AppointmentCostRevenueSummaryDto>> GetAppointmentCostRevenueSummary(Guid practitionerId, DateTime startDate, DateTime endDate);

    Task<AppointmentDetailDto> GetAppointmentDetail(Guid appointmentId);
}