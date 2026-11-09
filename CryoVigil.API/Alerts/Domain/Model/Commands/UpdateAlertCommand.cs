using CryoVigil.API.Alerts.Domain.Model.Entities;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;

namespace CryoVigil.API.Alerts.Domain.Model.Commands;

public record UpdateAlertCommand(
    int Id,
    string Code,
    string Title,
    string Message,
    AlertSeverity Severity,
    AlertStatus Status,
    AlertSourceType SourceType,
    string LaboratoryName,
    string? StorageUnitName,
    string? SensorCode,
    double? ReadingCelsius,
    double? SafeMinCelsius,
    double? SafeMaxCelsius,
    DateTime? AcknowledgedAt,
    string? AcknowledgedBy,
    DateTime? ResolvedAt,
    string? ResolvedBy,
    IEnumerable<CorrectiveAction>? CorrectiveActions,
    IEnumerable<AlertReading>? Readings);
