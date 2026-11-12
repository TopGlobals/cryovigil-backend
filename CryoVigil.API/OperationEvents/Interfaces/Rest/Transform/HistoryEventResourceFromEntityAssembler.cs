using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.OperationEvents.Interfaces.Rest.Resources;

namespace CryoVigil.API.OperationEvents.Interfaces.Rest.Transform;

public static class HistoryEventResourceFromEntityAssembler
{
    public static HistoryEventResource ToResourceFromEntity(HistoryEvent entity)
    {
        return new HistoryEventResource(
            entity.Id,
            entity.TitleKey,
            entity.Type,
            entity.MessageKey,
            entity.Severity,
            entity.Location,
            entity.Sensor,
            entity.IsResolved,
            entity.ActionsTakenKeys,
            entity.Timestamp,
            entity.Severity,
            entity.Location,
            entity.Sensor,
            entity.Timestamp
        );
    }
}
