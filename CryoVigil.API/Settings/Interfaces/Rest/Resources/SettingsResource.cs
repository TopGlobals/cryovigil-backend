namespace CryoVigil.API.Settings.Interfaces.Rest.Resources;

public record SettingsResource(
    int Id,
    string Key,
    ProfileSettingsResource Profile,
    GeneralPreferencesResource General,
    NotificationSettingsResource Notifications,
    SecuritySettingsResource Security,
    IEnumerable<MonitoringSensorResource> Sensors);
