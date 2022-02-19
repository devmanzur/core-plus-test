using CorePlus.Modules.Reporting.Models;

namespace CorePlus.Modules.Reporting.Interfaces;

public interface IAppointmentReportRepository
{
    Task CreateReportAsync(AppointmentRecord appointment);

    Task<List<MonthlyCostRevenueBucketDto>> GetMonthlyProfitSummary(List<int> practitionerIds, DateTime startDate,
        DateTime endDate);

    Task<List<MonthlyCostRevenueSummaryDto>> GetMonthlyProfitSummaryBySql(List<int> practitionerIds, DateTime startDate,
        DateTime endDate);

    Task<List<AppointmentCostRevenueSummaryDto>> GetPractitionerAppointmentBreakDown(Guid practitionerId, DateTime startDate, DateTime endDate);

}