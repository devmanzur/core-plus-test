using System.Text.Json;
using CorePlus.Modules.Appointments.Models;
using CorePlus.Modules.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;

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
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }

        await SeedInitialData(context, cancellationToken);
    }

    private async Task SeedInitialData(AppointmentDbContext context, CancellationToken cancellationToken)
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
            var appointments = GetAppointments();
            context.Appointments.AddRange(appointments!);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    #region Internal functions

    private List<Appointment>? GetAppointments()
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

    private List<Practitioner> GetPractitioners()
    {
        return new List<Practitioner>()
        {
            new Practitioner("Vanda Kirman"),
            new Practitioner("Zorah Heiss"),
            new Practitioner("Melodee Harriman"),
            new Practitioner("Moritz Spinks"),
            new Practitioner("Teresita Jacqueminot"),
            new Practitioner("Whitney Epilet"),
            new Practitioner("Maddalena Cortes"),
            new Practitioner("Cally Werner"),
            new Practitioner("Kalil Giannazzi"),
            new Practitioner("Jacinthe Phippard")
        };
    }

    #endregion
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