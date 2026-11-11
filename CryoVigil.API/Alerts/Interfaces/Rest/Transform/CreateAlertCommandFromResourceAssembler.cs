using CryoVigil.API.Alerts.Domain.Model.Commands;
using CryoVigil.API.Alerts.Domain.Model.Entities;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Alerts.Interfaces.Rest.Resources;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Transform;

public static class CreateAlertCommandFromResourceAssembler
{
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var correctiveActions = resource.CorrectiveActions?
            .Select(ca => new CorrectiveAction(ca.Description, ca.TechnicianName, ca.TakenAt ?? DateTime.UtcNow))
            .ToList();

        var readings = resource.Readings?
            .Select(r => new AlertReading(r.TakenAt, r.ValueCelsius))
            .ToList();

        return new CreateAlertCommand(
            resource.Code ?? string.Empty,
            resource.Title,
            resource.Message,
            resource.Severity,
            resource.Status ?? AlertStatus.Raised,
            resource.SourceType ?? AlertSourceType.ManualReport,
            resource.LaboratoryName,
            resource.StorageUnitName,
            resource.SensorCode,
            resource.ReadingCelsius,
            resource.SafeMinCelsius,
            resource.SafeMaxCelsius,
            resource.RaisedAt,
            resource.AcknowledgedAt,
            resource.AcknowledgedBy,
            resource.ResolvedAt,
            resource.ResolvedBy,
            correctiveActions,
            readings);
    }
}
