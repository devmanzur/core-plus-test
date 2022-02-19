using CorePlus.Modules.Reporting.Models;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportService
{
    public Task<List<AppointmentCostRevenueSummaryDto>> GetAppointmentCostRevenueSummary(Guid practitionerId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}