namespace CryoVigil.API.Alerts.Domain.Model.ValueObjects;

/// <summary>
/// Lifecycle states of an alert: Raised -> Acknowledged -> Closed.
/// </summary>
public enum AlertStatus
{
    Raised,
    Acknowledged,
    Closed
}
