namespace CryoVigil.API.History.Domain.Model.Commands;

public record CreateHistoryEventCommand(
    string EventCode,
    DateTime EventTime,
    string Severity,
    string SourceModule,
    string UserEmail,
    string EventAction,
    string Message
);