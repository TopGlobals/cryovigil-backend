using CryoVigil.API.Settings.Domain.Model.ValueObjects;

namespace CryoVigil.API.Settings.Domain.Model.Aggregates;

public class GeneralSettings
{
    public const int GlobalSettingsId = 1;

    private GeneralSettings()
    {
        Region = string.Empty;
        Key = "global";
        Profile = new ProfileSettings("Dr. Alex Vance", "alex.vance@topglobals.com", "Head Researcher");
        Notifications = new NotificationSettings(true, false, false, false, true, "2", "5");
        Security = new SecuritySettings(false);
        Sensors = [];
    }

    public GeneralSettings(string region, TemperatureUnit temperatureUnit)
    {
        Id = GlobalSettingsId;
        Key = "global";
        Profile = new ProfileSettings("Dr. Alex Vance", "alex.vance@topglobals.com", "Head Researcher");
        Notifications = new NotificationSettings(true, false, false, false, true, "2", "5");
        Security = new SecuritySettings(false);
        Sensors = [];
        Update(region, temperatureUnit);
    }

    public int Id { get; private set; }
    public string Key { get; private set; }
    public string Region { get; private set; }
    public TemperatureUnit TemperatureUnit { get; private set; }
    public ProfileSettings Profile { get; private set; }
    public NotificationSettings Notifications { get; private set; }
    public SecuritySettings Security { get; private set; }
    public ICollection<MonitoringSensor> Sensors { get; private set; }

    public static GeneralSettings CreateDefault()
    {
        return new GeneralSettings("Europe/Zurich", TemperatureUnit.Celsius);
    }

    public void UpdateSettings(SettingsDetails details)
    {
        ArgumentNullException.ThrowIfNull(details);
        ArgumentNullException.ThrowIfNull(details.Profile);
        ArgumentNullException.ThrowIfNull(details.Notifications);
        ArgumentNullException.ThrowIfNull(details.Sensors);

        Update(details.Region, details.TemperatureUnit);
        Key = "global";
        Profile.Update(details.Profile.Name, details.Profile.Email, details.Profile.Role);
        Notifications.Update(
            details.Notifications.Email,
            details.Notifications.InApp,
            details.Notifications.DailySummary,
            details.Notifications.WeeklyReport,
            details.Notifications.InstantAlerts,
            details.Notifications.WarningThreshold,
            details.Notifications.CriticalThreshold);
        Security.Update(details.TwoFactorEnabled);

        var sensorsById = Sensors.ToDictionary(sensor => sensor.Id);
        var requestedSensorIds = new HashSet<Guid>();
        foreach (var sensorDetails in details.Sensors)
        {
            if (!requestedSensorIds.Add(sensorDetails.Id))
                throw new ArgumentException($"Duplicate sensor ID: {sensorDetails.Id}", nameof(details));

            if (sensorsById.TryGetValue(sensorDetails.Id, out var sensor))
            {
                sensor.Update(
                    sensorDetails.Name,
                    sensorDetails.Model,
                    sensorDetails.Serial,
                    sensorDetails.Type,
                    sensorDetails.Location,
                    sensorDetails.Status);
            }
            else
            {
                Sensors.Add(new MonitoringSensor(
                    sensorDetails.Id,
                    sensorDetails.Name,
                    sensorDetails.Model,
                    sensorDetails.Serial,
                    sensorDetails.Type,
                    sensorDetails.Location,
                    sensorDetails.Status));
            }
        }

        foreach (var sensor in Sensors.Where(sensor => !requestedSensorIds.Contains(sensor.Id)).ToList())
            Sensors.Remove(sensor);
    }

    public void Update(string region, TemperatureUnit temperatureUnit)
    {
        if (string.IsNullOrWhiteSpace(region))
            throw new ArgumentException("Region is required.", nameof(region));

        var normalizedRegion = region.Trim();
        if (normalizedRegion.Length > 100)
            throw new ArgumentException("Region cannot exceed 100 characters.", nameof(region));

        if (!Enum.IsDefined(typeof(TemperatureUnit), temperatureUnit))
            throw new ArgumentOutOfRangeException(nameof(temperatureUnit));

        Region = normalizedRegion;
        TemperatureUnit = temperatureUnit;
    }
}
