using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Persistence;
using CorePlus.Modules.Appointments.UseCases;
using CorePlus.Modules.Reporting.Interfaces;
using CorePlus.Modules.Reporting.Perisistence;
using CorePlus.Modules.Reporting.UseCases;
using CorePlus.Modules.Shared.Interfaces;
using MediatR;

namespace CorePlus.Web.Utils;

public static class DependencyExtensions
{
    public static IServiceCollection AddElasticSearchClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection("ElasticConfiguration");
        var client = ElasticClientBuilder.CreateClient()
            .WithCredentials(config["Server"], config["Username"], config["Password"])
            .Create();

        services.AddScoped(sp => client);
        return services;
    }

    public static IServiceCollection AddReportModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddElasticSearchClient(configuration);
        services.AddScoped<IAppointmentReportRepository, AppointmentReportRepository>();
        return services;
    }

    public static IServiceCollection AddAppointmentModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IPractitionerService, PractitionerService>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddDbContext<AppointmentDbContext>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetService<AppointmentDbContext>()!);
        services.AddMediatR(typeof(BaseEntity));
        return services;
    }
}