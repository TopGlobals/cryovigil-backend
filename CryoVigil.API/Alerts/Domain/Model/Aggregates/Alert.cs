using CryoVigil.API.Alerts.Domain.Model.Entities;
using CryoVigil.API.Alerts.Domain.Model.Events;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Shared.Domain.Model.Aggregates.Internal;

namespace CryoVigil.API.Alerts.Domain.Model.Aggregates;

/// <summary>
/// Aggregate root representing an alert/incident within the laboratory monitoring environment.
/// Coordinates lifecycle transitions (Raised -> Acknowledged -> Closed) and encapsulates
/// audit trails (corrective actions) and sensor readings.
/// </summary>
public class Alert : AggregateRoot
{
    public const int MaxTitleLength = 80;
    public const int MaxMessageLength = 500;
    public const int MinMessageLength = 10;

    private readonly List<CorrectiveAction> _correctiveActions = [];
    private readonly List<AlertReading> _readings = [];

    public AlertId Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public AlertSeverity Severity { get; private set; }
    public AlertStatus Status { get; private set; }
    public AlertSourceType SourceType { get; private set; }
    public string LaboratoryName { get; private set; } = string.Empty;
    public string? StorageUnitName { get; private set; }
    public string? SensorCode { get; private set; }
    public double? ReadingCelsius { get; private set; }
    public double? SafeMinCelsius { get; private set; }
    public double? SafeMaxCelsius { get; private set; }
    public DateTime RaisedAt { get; private set; }
    public DateTime? AcknowledgedAt { get; private set; }
    public string? AcknowledgedBy { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public string? ResolvedBy { get; private set; }

    public IReadOnlyCollection<CorrectiveAction> CorrectiveActions => _correctiveActions.AsReadOnly();
    public IReadOnlyCollection<AlertReading> Readings => _readings.AsReadOnly();

    protected Alert() { }

    public Alert(
        string code,
        string title,
        string message,
        AlertSeverity severity,
        AlertStatus status,
        AlertSourceType sourceType,
        string laboratoryName,
        string? storageUnitName = null,
        string? sensorCode = null,
        double? readingCelsius = null,
        double? safeMinCelsius = null,
        double? safeMaxCelsius = null,
        DateTime? raisedAt = null,
        DateTime? acknowledgedAt = null,
        string? acknowledgedBy = null,
        DateTime? resolvedAt = null,
        string? resolvedBy = null,
        IEnumerable<CorrectiveAction>? correctiveActions = null,
        IEnumerable<AlertReading>? readings = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        ArgumentException.ThrowIfNullOrWhiteSpace(laboratoryName);

        Code = code.Trim();
        Title = title.Trim();
        Message = message.Trim();
        Severity = severity;
        Status = status;
        SourceType = sourceType;
        LaboratoryName = laboratoryName.Trim();
        StorageUnitName = string.IsNullOrWhiteSpace(storageUnitName) ? null : storageUnitName.Trim();
        SensorCode = string.IsNullOrWhiteSpace(sensorCode) ? null : sensorCode.Trim();
        ReadingCelsius = readingCelsius;
        SafeMinCelsius = safeMinCelsius;
        SafeMaxCelsius = safeMaxCelsius;
        RaisedAt = raisedAt ?? DateTime.UtcNow;
        AcknowledgedAt = acknowledgedAt;
        AcknowledgedBy = string.IsNullOrWhiteSpace(acknowledgedBy) ? null : acknowledgedBy.Trim();
        ResolvedAt = resolvedAt;
        ResolvedBy = string.IsNullOrWhiteSpace(resolvedBy) ? null : resolvedBy.Trim();

        if (correctiveActions is not null)
            _correctiveActions.AddRange(correctiveActions);

        if (readings is not null)
            _readings.AddRange(readings);

        AddDomainEvent(new AlertCreatedEvent(Id, Code));
    }

    public void Acknowledge(string acknowledgedBy, DateTime acknowledgedAt)
    {
        if (Status != AlertStatus.Raised)
            throw new InvalidOperationException($"Cannot acknowledge alert in status '{Status}'.");

        Status = AlertStatus.Acknowledged;
        AcknowledgedBy = acknowledgedBy.Trim();
        AcknowledgedAt = acknowledgedAt;

        AddDomainEvent(new AlertAcknowledgedEvent(Id, AcknowledgedBy));
    }

    public void Resolve(string description, string technicianName, DateTime resolvedAt)
    {
        if (Status == AlertStatus.Closed)
            throw new InvalidOperationException("Alert is already closed.");

        if (Severity == AlertSeverity.Critical && string.IsNullOrWhiteSpace(description))
            throw new InvalidOperationException("Critical alerts require a corrective action note to resolve.");

        if (!string.IsNullOrWhiteSpace(description))
        {
            _correctiveActions.Add(new CorrectiveAction(description, technicianName, resolvedAt));
        }

        if (Status == AlertStatus.Raised)
        {
            AcknowledgedBy = technicianName;
            AcknowledgedAt = resolvedAt;
        }

        Status = AlertStatus.Closed;
        ResolvedBy = technicianName;
        ResolvedAt = resolvedAt;

        AddDomainEvent(new AlertResolvedEvent(Id, ResolvedBy));
    }

    public void Update(
        string title,
        string message,
        AlertSeverity severity,
        AlertStatus status,
        AlertSourceType sourceType,
        string laboratoryName,
        string? storageUnitName,
        string? sensorCode,
        double? readingCelsius,
        double? safeMinCelsius,
        double? safeMaxCelsius,
        DateTime? acknowledgedAt,
        string? acknowledgedBy,
        DateTime? resolvedAt,
        string? resolvedBy,
        IEnumerable<CorrectiveAction>? correctiveActions,
        IEnumerable<AlertReading>? readings)
    {
        Title = title.Trim();
        Message = message.Trim();
        Severity = severity;
        Status = status;
        SourceType = sourceType;
        LaboratoryName = laboratoryName.Trim();
        StorageUnitName = string.IsNullOrWhiteSpace(storageUnitName) ? null : storageUnitName.Trim();
        SensorCode = string.IsNullOrWhiteSpace(sensorCode) ? null : sensorCode.Trim();
        ReadingCelsius = readingCelsius;
        SafeMinCelsius = safeMinCelsius;
        SafeMaxCelsius = safeMaxCelsius;
        AcknowledgedAt = acknowledgedAt;
        AcknowledgedBy = string.IsNullOrWhiteSpace(acknowledgedBy) ? null : acknowledgedBy.Trim();
        ResolvedAt = resolvedAt;
        ResolvedBy = string.IsNullOrWhiteSpace(resolvedBy) ? null : resolvedBy.Trim();

        if (correctiveActions is not null)
        {
            _correctiveActions.Clear();
            _correctiveActions.AddRange(correctiveActions);
        }

        if (readings is not null)
        {
            _readings.Clear();
            _readings.AddRange(readings);
        }
    }
}
