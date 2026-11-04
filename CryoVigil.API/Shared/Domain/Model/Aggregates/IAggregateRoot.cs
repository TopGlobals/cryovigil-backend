using CryoVigil.API.Shared.Domain.Model.Events;

namespace CryoVigil.API.Shared.Domain.Model.Aggregates;

public interface IAggregateRoot
{
    IReadOnlyCollection<IEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
