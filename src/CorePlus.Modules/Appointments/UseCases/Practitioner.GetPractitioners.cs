using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using Microsoft.EntityFrameworkCore;

namespace CorePlus.Modules.Appointments.UseCases;

public class PractitionerService : IPractitionerService
{
    private readonly IUnitOfWork _unitOfWork;

    public PractitionerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<List<PractitionerDto>> GetPractitioners()
    {
        return _unitOfWork.Practitioners.AsNoTracking()
            .Select(x => new PractitionerDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

    }
}