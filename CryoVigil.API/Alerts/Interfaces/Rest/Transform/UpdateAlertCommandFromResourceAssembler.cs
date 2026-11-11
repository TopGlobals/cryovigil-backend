using CryoVigil.API.Alerts.Domain.Model.Commands;
using CryoVigil.API.Alerts.Domain.Model.Entities;
using CryoVigil.API.Alerts.Interfaces.Rest.Resources;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Transform;

public static class UpdateAlertCommandFromResourceAssembler
{
    public static UpdateAlertCommand ToCommandFromResource(int id, UpdateAlertResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        var correctiveActions = resource.CorrectiveActions?
            .Select(ca => new CorrectiveAction(ca.Description, ca.TechnicianName, ca.TakenAt ?? DateTime.UtcNow))
            .ToList();

        var readings = resource.Readings?
            .Select(r => new AlertReading(r.TakenAt, r.ValueCelsius))
            .ToList();

        return new UpdateAlertCommand(
            id,
            resource.Code ?? string.Empty,
            resource.Title,
            resource.Message,
            resource.Severity,
            resource.Status,
            resource.SourceType,
            resource.LaboratoryName,
            resource.StorageUnitName,
            resource.SensorCode,
            resource.ReadingCelsius,
            resource.SafeMinCelsius,
            resource.SafeMaxCelsius,
            resource.AcknowledgedAt,
            resource.AcknowledgedBy,
            resource.ResolvedAt,
            resource.ResolvedBy,
            correctiveActions,
            readings);
    }
}
