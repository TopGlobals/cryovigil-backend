namespace CryoVigil.API.History.Interfaces.Rest.Resources;

public record CreateHistoryEventResource(
    string EventCode,
    DateTime EventTime,
    string Severity,
    string SourceModule,
    string UserEmail,
    string EventAction,
    string Message
);