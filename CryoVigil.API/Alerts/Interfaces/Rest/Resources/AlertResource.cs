using System.Text.Json.Serialization;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Resources;

public record AlertResource(
    int Id,
    string Code,
    string Title,
    string Message,
    [property: JsonConverter(typeof(KebabCaseEnumConverter<AlertSeverity>))]
    AlertSeverity Severity,
    [property: JsonConverter(typeof(KebabCaseEnumConverter<AlertStatus>))]
    AlertStatus Status,
    [property: JsonConverter(typeof(KebabCaseEnumConverter<AlertSourceType>))]
    AlertSourceType SourceType,
    string LaboratoryName,
    string? StorageUnitName,
    string? SensorCode,
    double? ReadingCelsius,
    double? SafeMinCelsius,
    double? SafeMaxCelsius,
    DateTime? RaisedAt,
    DateTime? AcknowledgedAt,
    string? AcknowledgedBy,
    DateTime? ResolvedAt,
    string? ResolvedBy,
    IEnumerable<CorrectiveActionResource> CorrectiveActions,
    IEnumerable<AlertReadingResource> Readings);
