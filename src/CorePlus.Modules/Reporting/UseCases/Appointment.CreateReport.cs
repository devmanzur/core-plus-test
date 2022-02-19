using CorePlus.Modules.Reporting.Interfaces;
using CorePlus.Modules.Reporting.Models;
using Nest;

namespace CorePlus.Modules.Reporting.UseCases;

public partial class AppointmentReportRepository : IAppointmentReportRepository
{
    public async Task CreateReportAsync(AppointmentRecord appointment)
    {
        await EnsureIndexExists();

        await _elasticClient.IndexAsync(
            appointment,
            idx => idx.Index(ReportIndex()));
    }
}