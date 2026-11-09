using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Shared.Domain.Model.Events;

namespace CryoVigil.API.Alerts.Domain.Model.Events;

public record AlertAcknowledgedEvent(AlertId AlertId, string AcknowledgedBy) : IEvent;
