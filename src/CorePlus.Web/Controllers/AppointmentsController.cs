using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using CorePlus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorePlus.Web.Controllers;

public class AppointmentsController : BaseApiController
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<AppointmentDto>>> Create([FromBody] AppointmentDto request)
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
}