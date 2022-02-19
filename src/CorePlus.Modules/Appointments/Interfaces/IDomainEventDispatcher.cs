using CorePlus.Modules.Shared.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CorePlus.Modules.Appointments.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(List<EntityEntry<AggregateRoot>> changes);

}