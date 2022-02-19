using CorePlus.Modules.Appointments.Models;
using Microsoft.EntityFrameworkCore;

namespace CorePlus.Modules.Appointments.Interfaces;

public interface IUnitOfWork
{
    DbSet<Practitioner> Practitioners { get; set; }
    Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken());
}