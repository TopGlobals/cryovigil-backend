using CryoVigil.API.Alerts.Domain.Model.ValueObjects;

namespace CryoVigil.API.Alerts.Domain.Model.Aggregates;

public class Alert
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AlertSeverity Severity { get; set; }
    public AlertStatus Status { get; set; }
    public int LaboratoryId { get; set; }
    public int SensorId { get; set; }
    public float Reading { get; set; }
    public float SafeMin { get; set; }
    public float SafeMax { get; set; }
    public DateTime RaisedAt { get; set; }
}