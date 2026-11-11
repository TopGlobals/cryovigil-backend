using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Interfaces.Rest.Resources;

namespace CryoVigil.API.History.Interfaces.Rest.Transform;

public static class HistoryEventResourceFromEntityAssembler
{
    public static HistoryEventResource ToResourceFromEntity(HistoryEvent entity)
    {
        return new HistoryEventResource(
            entity.Id,
            entity.EventCode,
            entity.EventTime,
            entity.Severity,
            entity.SourceModule,
            entity.UserEmail,
            entity.EventAction,
            entity.Message
        );
    }
}