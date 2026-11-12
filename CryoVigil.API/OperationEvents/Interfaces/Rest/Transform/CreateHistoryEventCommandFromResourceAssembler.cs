using CryoVigil.API.OperationEvents.Domain.Model.Commands;
using CryoVigil.API.OperationEvents.Interfaces.Rest.Resources;

namespace CryoVigil.API.OperationEvents.Interfaces.Rest.Transform;

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
