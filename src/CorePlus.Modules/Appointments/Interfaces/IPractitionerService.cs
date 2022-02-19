using CorePlus.Modules.Appointments.Models;

namespace CorePlus.Modules.Appointments.Interfaces;

public interface IPractitionerService
{
    Task<List<PractitionerDto>> GetPractitioners();
}