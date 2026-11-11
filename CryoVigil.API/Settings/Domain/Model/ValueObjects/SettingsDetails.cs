namespace CryoVigil.API.Settings.Domain.Model.ValueObjects;

public record NotificationSettingsDetails(
    bool Email,
    bool InApp,
    bool DailySummary,
    bool WeeklyReport,
    bool InstantAlerts,
    string WarningThreshold,
    string CriticalThreshold);

public record MonitoringSensorDetails(
    Guid Id,
    string Name,
    string Model,
    string Serial,
    string Type,
    string Location,
    string Status);

public record SettingsDetails(
    string Region,
    TemperatureUnit TemperatureUnit,
    NotificationSettingsDetails Notifications,
    bool TwoFactorEnabled,
    IReadOnlyCollection<MonitoringSensorDetails> Sensors);
