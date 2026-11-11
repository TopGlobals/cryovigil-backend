namespace CryoVigil.API.History.Domain.Model.Commands;

public record CreateHistoryEventCommand(
    string TitleKey,
    string Type,
    string MessageKey,
    string Severity,
    string Location,
    string Sensor,
    bool IsResolved,
    IEnumerable<string>? ActionsTakenKeys = null,
    DateTime? Timestamp = null
);
