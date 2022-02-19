using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using CorePlus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorePlus.Web.Controllers;

public class PractitionersController : BaseApiController
{
    private readonly IPractitionerService _practitionerService;

    public PractitionersController(IPractitionerService practitionerService)
    {
        _practitionerService = practitionerService;
    }

    [HttpGet]
    public async Task<ActionResult<Envelope<List<PractitionerDto>>>> GetPractitioners()
    {
        var practitioners = await _practitionerService.GetPractitioners();
        return Ok(Envelope<List<PractitionerDto>>.Ok(practitioners));
    }
}