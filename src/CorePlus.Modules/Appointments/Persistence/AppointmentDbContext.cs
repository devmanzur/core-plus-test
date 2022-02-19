using System.Reflection;
using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Appointments.Models;
using CorePlus.Modules.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace CorePlus.Modules.Appointments.Persistence;

public class AppointmentDbContext : DbContext, IUnitOfWork
{
    private readonly IConfiguration _configuration;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    public DbSet<Practitioner> Practitioners { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    public AppointmentDbContext(IConfiguration configuration, IDomainEventDispatcher domainEventDispatcher)
    {
        _configuration = configuration;
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("appointments");
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppointmentDbContext))!);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseSqlServer(_configuration.GetConnectionString("CorePlusDb"));
    }
    
    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        var changes = TrackChanges();
        var changesMade = await base.SaveChangesAsync(true, cancellationToken);
        if (changesMade > 0) await _domainEventDispatcher.DispatchEventsAsync(changes);
        return changesMade;
    }
    
    private List<EntityEntry<AggregateRoot>> TrackChanges()
    {
        var changes = this.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x =>
                HasDomainEvents(x) || HasBeenAddedOrRemoved(x)
            ).ToList();
            
        return changes;
    }
    
    private static bool HasDomainEvents(EntityEntry<AggregateRoot> x)
    {
        return x.Entity.DomainEvents.Any();
    }

    private static bool HasBeenAddedOrRemoved(EntityEntry<AggregateRoot> x)
    {
        return x.State is EntityState.Added or EntityState.Deleted;
    }

}