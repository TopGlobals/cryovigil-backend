using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Interfaces.Rest.Resources;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Transform;

public static class AlertResourceFromEntityAssembler
{
    public static AlertResource ToResourceFromEntity(Alert entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var correctiveActions = entity.CorrectiveActions
            .Select(ca => new CorrectiveActionResource(ca.Description, ca.TechnicianName, ca.TakenAt))
            .ToList();

        var readings = entity.Readings
            .Select(r => new AlertReadingResource(r.TakenAt, r.ValueCelsius))
            .ToList();

        return new AlertResource(
            entity.Id.Value,
            entity.Code,
            entity.Title,
            entity.Message,
            entity.Severity,
            entity.Status,
            entity.SourceType,
            entity.LaboratoryName,
            entity.StorageUnitName,
            entity.SensorCode,
            entity.ReadingCelsius,
            entity.SafeMinCelsius,
            entity.SafeMaxCelsius,
            entity.RaisedAt,
            entity.AcknowledgedAt,
            entity.AcknowledgedBy,
            entity.ResolvedAt,
            entity.ResolvedBy,
            correctiveActions,
            readings);
    }
}
