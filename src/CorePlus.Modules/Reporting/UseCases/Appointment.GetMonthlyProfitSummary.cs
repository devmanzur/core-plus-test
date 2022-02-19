using CorePlus.Modules.Reporting.Models;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportRepository
{
    public Task<List<PractitionerMonthlyCostRevenueSummaryDto>> GetMonthlyProfitSummary(List<Guid> practitionerIds, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}