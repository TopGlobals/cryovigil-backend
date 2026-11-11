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
                Email = settings.Notifications.Email,
                InApp = settings.Notifications.InApp,
                DailySummary = settings.Notifications.DailySummary,
                WeeklyReport = settings.Notifications.WeeklyReport,
                InstantAlerts = settings.Notifications.InstantAlerts,
                WarningThreshold = settings.Notifications.WarningThreshold,
                CriticalThreshold = settings.Notifications.CriticalThreshold
            },
            new SecuritySettingsResource
            {
                TwoFactorEnabled = settings.Security.TwoFactorEnabled
            },
            settings.Sensors.Select(ToResource));
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
