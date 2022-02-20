using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using CorePlus.Modules.Reporting.Interfaces;
using CorePlus.Modules.Reporting.Models;
using CorePlus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorePlus.Web.Controllers;

public class AppointmentsController : BaseApiController
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAppointmentReportRepository _appointmentReportRepository;

    public AppointmentsController(IAppointmentService appointmentService, IAppointmentReportRepository appointmentReportRepository)
    {
        _appointmentService = appointmentService;
        _appointmentReportRepository = appointmentReportRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<AppointmentDto>>> Create([FromBody] AppointmentCreateDto request)
    {
        var createAppointment = await _appointmentService.CreateAppointment(request);
        if (createAppointment.IsSuccess)
        {
            return Ok(Envelope<AppointmentDto>.Ok(createAppointment.Value));
        }

        return BadRequest(Envelope.Error(createAppointment.Error));
    }
    
    [HttpGet("{appointmentId}")]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentDetails(Guid appointmentId)
    {
        var appointment = await _appointmentService.GetAppointmentDetail(appointmentId);
        if (appointment == null)
        {
            return BadRequest(Envelope.Error("Not found!"));
        }
        
        return Ok(Envelope<AppointmentDto>.Ok(appointment));
    }
    
    [HttpGet("profit-reports")]
    public async Task<ActionResult<Envelope<List<MonthlyCostRevenueSummaryDto>>>> GetProfitReport([FromQuery] SummaryQueryModel request)
    {
        var summary =
            await _appointmentReportRepository.GetMonthlyProfitSummary(request.Practitioners, request.Start,
                request.End);

        return Ok(Envelope<List<MonthlyCostRevenueSummaryDto>>.Ok(summary));
    }

    [HttpGet]
    public async Task<ActionResult<Envelope<List<AppointmentCostRevenueSummaryDto>>>> GetPractitionerAppointments([FromQuery] PractitionerSummaryQueryModel request)
    {
        var start = new DateTime(request.Month.Year, request.Month.Month, 1);
        var end = start.AddMonths(1).AddDays(-1);
        
        var appointments =
            await _appointmentReportRepository.GetAppointments(request.PractitionerId, start, end);
        
        return Ok(Envelope<List<AppointmentCostRevenueSummaryDto>>.Ok(appointments));
    }
}