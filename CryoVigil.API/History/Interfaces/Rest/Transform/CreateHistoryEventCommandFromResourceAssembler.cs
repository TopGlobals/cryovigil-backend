using CryoVigil.API.History.Domain.Model.Commands;
using CryoVigil.API.History.Interfaces.Rest.Resources;

namespace CryoVigil.API.History.Interfaces.Rest.Transform;

public static class CreateHistoryEventCommandFromResourceAssembler
{
    public static CreateHistoryEventCommand ToCommandFromResource(CreateHistoryEventResource resource)
    {
        return new CreateHistoryEventCommand(
            resource.EventCode,
            resource.EventTime,
            resource.Severity,
            resource.SourceModule,
            resource.UserEmail,
            resource.EventAction,
            resource.Message
        );
    }
}