namespace CryoVigil.API.Alerts.Domain.Model.ValueObjects;

/// <summary>
/// Domain events or sources that can originate an alert.
/// </summary>
public enum AlertSourceType
{
    ThermalExcursion,
    PreventiveTrend,
    ThermalStatusNormalized,
    CalibrationDue,
    ManualReport
}
