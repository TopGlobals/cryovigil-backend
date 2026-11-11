using CryoVigil.API.History.Domain.Model.Commands;
using CryoVigil.API.History.Interfaces.Rest.Resources;

namespace CryoVigil.API.History.Interfaces.Rest.Transform;

public static class CreateHistoryEventCommandFromResourceAssembler
{
    public static CreateHistoryEventCommand ToCommandFromResource(CreateHistoryEventResource resource)
    {
        return new CreateHistoryEventCommand(
            resource.TitleKey,
            resource.Type,
            resource.MessageKey,
            resource.Severity,
            resource.Location,
            resource.Sensor,
            resource.IsResolved,
            resource.ActionsTakenKeys,
            resource.Timestamp
        );
    }
}
