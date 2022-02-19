using CorePlus.Modules.Reporting.Models;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportRepository
{
    public Task<List<AppointmentCostRevenueSummaryDto>> GetPractitionerAppointmentBreakDown(Guid practitionerId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}