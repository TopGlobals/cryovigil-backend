using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Shared.Domain.Model.Events;

namespace CryoVigil.API.Alerts.Domain.Model.Events;

public record AlertResolvedEvent(AlertId AlertId, string ResolvedBy) : IEvent;
