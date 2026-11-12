namespace CryoVigil.API.OperationEvents.Interfaces.Rest.Resources;

public record HistoryEventResource(
    int Id,
    string TitleKey,
    string Type,
    string MessageKey,
    string Severity,
    string Location,
    string Sensor,
    bool IsResolved,
    IEnumerable<string> ActionsTakenKeys,
    DateTime Timestamp,
    string? SeverityLevel = null,
    string? LocationName = null,
    string? SensorCode = null,
    DateTime? CreatedAt = null
);
