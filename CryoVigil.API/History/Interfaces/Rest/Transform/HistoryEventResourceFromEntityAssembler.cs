using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Interfaces.Rest.Resources;

namespace CryoVigil.API.History.Interfaces.Rest.Transform;

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
