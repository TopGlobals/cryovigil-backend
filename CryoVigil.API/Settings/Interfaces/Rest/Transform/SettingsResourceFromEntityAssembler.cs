using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.ValueObjects;
using CryoVigil.API.Settings.Interfaces.Rest.Resources;

namespace CryoVigil.API.Settings.Interfaces.Rest.Transform;

public static class SettingsResourceFromEntityAssembler
{
    public static SettingsResource ToResource(GeneralSettings settings)
    {
        return new SettingsResource(
            settings.Id,
            settings.Key,
            new GeneralPreferencesResource
            {
                Region = settings.Region,
                TemperatureUnit = settings.TemperatureUnit.ToString()
            },
            new NotificationSettingsResource
            {
                Email = settings.Notifications?.Email ?? false,
                InApp = settings.Notifications?.InApp ?? false,
                DailySummary = settings.Notifications?.DailySummary ?? false,
                WeeklyReport = settings.Notifications?.WeeklyReport ?? false,
                InstantAlerts = settings.Notifications?.InstantAlerts ?? false,
                WarningThreshold = settings.Notifications?.WarningThreshold ?? "2",
                CriticalThreshold = settings.Notifications?.CriticalThreshold ?? "5"
            },
            new SecuritySettingsResource
            {
                TwoFactorEnabled = settings.Security?.TwoFactorEnabled ?? false
            },
            (settings.Sensors ?? []).Select(ToResource));
    }

    private static MonitoringSensorResource ToResource(MonitoringSensor sensor)
    {
        return new MonitoringSensorResource
        {
            Id = sensor.Id,
            Name = sensor.Name,
            Model = sensor.Model,
            Serial = sensor.Serial,
            Type = sensor.Type,
            Location = sensor.Location,
            Status = sensor.Status
        };
    }
}
