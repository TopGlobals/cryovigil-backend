namespace CryoVigil.API.History.Interfaces.Rest.Resources;

public record HistoryEventResource(
    int Id,
    string EventCode,
    DateTime EventTime,
    string Severity,
    string SourceModule,
    string UserEmail,
    string EventAction,
    string Message
);