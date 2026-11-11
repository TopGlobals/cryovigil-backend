namespace CryoVigil.API.Alerts.Domain.Model.ValueObjects;

/// <summary>
/// Severity levels of an alert, ordered from most to least urgent.
/// </summary>
public enum AlertSeverity
{
    Critical,
    Warning,
    Info
}
