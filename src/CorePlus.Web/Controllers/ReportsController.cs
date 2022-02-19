using CorePlus.Modules.Reporting.Interfaces;
using CorePlus.Modules.Reporting.Models;
using CorePlus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorePlus.Web.Controllers;

public class ReportsController : BaseApiController
{
    private readonly IAppointmentReportRepository _appointmentReportRepository;

    public ReportsController(IAppointmentReportRepository appointmentReportRepository)
    {
        _appointmentReportRepository = appointmentReportRepository;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<Envelope<List<MonthlyCostRevenueSummaryDto>>>> GetSummary([FromQuery] SummaryQueryModel request)
    {
        if (request.Source == "sql")
        {
            var sqlSummary =
                await _appointmentReportRepository.GetMonthlyProfitSummaryBySql(request.Practitioners, request.Start,
                    request.End);

            return Ok(Envelope<List<MonthlyCostRevenueSummaryDto>>.Ok(sqlSummary));
        }

        var summary =
            await _appointmentReportRepository.GetMonthlyProfitSummary(request.Practitioners, request.Start,
                request.End);

        return Ok(Envelope<List<MonthlyCostRevenueBucketDto>>.Ok(summary));
    }
}