using CorePlus.Modules.Appointments.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CorePlus.Modules.Appointments.Interfaces;

public interface IUnitOfWork
{
    DbSet<Practitioner> Practitioners { get; set; }
    DbSet<Appointment> Appointments { get; set; }
    Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken());
    Task<List<T?>> GetListAsync<T>(string query, params SqlParameter[] parameter) where T : class;
}