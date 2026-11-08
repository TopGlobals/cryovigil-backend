using CryoVigil.API.Settings.Domain.Model.ValueObjects;

namespace CryoVigil.API.Settings.Domain.Model.Aggregates;

public class GeneralSettings
{
    public const int GlobalSettingsId = 1;

    private GeneralSettings()
    {
        Region = string.Empty;
    }

    public GeneralSettings(string region, TemperatureUnit temperatureUnit)
    {
        Id = GlobalSettingsId;
        Update(region, temperatureUnit);
    }

    public int Id { get; private set; }
    public string Region { get; private set; }
    public TemperatureUnit TemperatureUnit { get; private set; }

    public static GeneralSettings CreateDefault()
    {
        return new GeneralSettings("Europe/Zurich", TemperatureUnit.Celsius);
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
