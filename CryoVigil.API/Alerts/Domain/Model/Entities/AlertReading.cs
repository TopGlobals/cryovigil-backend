using CryoVigil.API.Alerts.Domain.Model.ValueObjects;

namespace CryoVigil.API.Alerts.Domain.Model.Entities;

/// <summary>
/// Sensor reading captured leading up to or during an alert event.
/// </summary>
public class AlertReading
{
    public int Id { get; private set; }
    public AlertId AlertId { get; private set; }
    public DateTime TakenAt { get; private set; }
    public double ValueCelsius { get; private set; }

    protected AlertReading() { }

    public AlertReading(DateTime takenAt, double valueCelsius)
    {
        TakenAt = takenAt;
        ValueCelsius = valueCelsius;
    }
}
