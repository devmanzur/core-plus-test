using CorePlus.Modules.Reporting.Models;

namespace CorePlus.Modules.Reporting.Interfaces;

public interface IAppointmentReportService
{
    Task CreateReportAsync(AppointmentRecord appointment);

    Task<List<MonthlyCostRevenueSummaryDto>> GetMonthlyProfitSummary(List<long> practitionerIds, DateTime startDate,
        DateTime endDate);

    Task<List<AppointmentCostRevenueSummaryDto>> GetAppointments(long practitionerId, DateTime startDate, DateTime endDate);

}