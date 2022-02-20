using System.Text.Json;
using CorePlus.Modules.Appointments.Models;
using CorePlus.Modules.Appointments.Persistence;
using CorePlus.Modules.Reporting.Models;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace CorePlus.Web.Utils;

public class DatabaseSeedingService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseSeedingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppointmentDbContext>();
        var elasticClient = scope.ServiceProvider.GetRequiredService<ElasticClient>();
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }

        await SeedInitialData(context,elasticClient, cancellationToken);
    }

    private async Task SeedInitialData(AppointmentDbContext context, ElasticClient elasticClient,CancellationToken cancellationToken)
    {
        var practitionerExists = await context.Practitioners.AnyAsync(cancellationToken: cancellationToken);
        if (!practitionerExists)
        {
            var practitioners = GetPractitioners();
            context.Practitioners.AddRange(practitioners);
            await context.SaveChangesAsync(cancellationToken);
        }

        var appointmentsExists = await context.Appointments.AnyAsync(cancellationToken);
        if (!appointmentsExists)
        {
            var practitioners = ParsePractitioners();

            var appointments = ParseAppointments();
            context.Appointments.AddRange(appointments!);

            var records = appointments.Select(x => new AppointmentRecord()
            {
                Cost = x.Cost,
                Date = x.Date,
                UniqueId = x.UniqueId,
                Duration = x.Duration,
                Revenue = x.Revenue,
                AppointmentType = x.AppointmentType,
                ClientName = x.ClientName,
                Practitioner = new PractitionerRecord()
                {
                    Name = practitioners.FirstOrDefault(p=>p.id == x.PractitionerId).name,
                    Id = x.PractitionerId
                }
            });

            await elasticClient.IndexManyAsync(records, $"appointments_{DateTime.UtcNow:yyyy.MM.dd}", cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    #region Internal functions

    private List<Appointment>? ParseAppointments()
    {
        var appointmentJson = File.ReadAllText("appointments.json");
        var appointments = JsonSerializer.Deserialize<List<AppointmentResponse>>(appointmentJson);

        return appointments?
            .Select(x =>
                new Appointment(DateTime.Parse(x.date), x.client_name, x.appointment_type, x.duration, x.cost,
                    x.revenue)
                {
                    PractitionerId = x.practitioner_id
                }).ToList();
    }

    private List<PractitionerResponse>? ParsePractitioners()
    {
        var practitionerJson = File.ReadAllText("practitioners.json");
        var practitioners = JsonSerializer.Deserialize<List<PractitionerResponse>>(practitionerJson);
        return practitioners;
    }

    private List<Practitioner>? GetPractitioners()
    {
        return ParsePractitioners()?.Select(x=> new Practitioner(x.name)).ToList();
    }

    #endregion
}

internal class PractitionerResponse
{
    public string name { get; set; }
    public int id { get; set; }
}

internal class AppointmentResponse
{
    public string date { get; set; }
    public string client_name { get; set; }
    public string appointment_type { get; set; }
    public int duration { get; set; }
    public int revenue { get; set; }
    public int cost { get; set; }
    public int practitioner_id { get; set; }
}