using CorePlus.Modules.Appointments.Interfaces;
using CorePlus.Modules.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CorePlus.Modules.Appointments.Persistence;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task DispatchEventsAsync(List<EntityEntry<AggregateRoot>> changes)
    {
        var domainEvents = changes
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        ClearRecords(changes);

        var tasks = domainEvents
            .Select(async domainEvent => { await _mediator.Publish(domainEvent); });
        await Task.WhenAll(tasks);
    }

    private static void ClearRecords(List<EntityEntry<AggregateRoot>> changes)
    {
        changes
            .ForEach(entity =>
            {
                entity.Entity.ClearDomainEvents();
                entity.State = EntityState.Detached;
            });
    }
}

