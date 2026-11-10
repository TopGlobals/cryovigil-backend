using System.ComponentModel.DataAnnotations;

namespace CryoVigil.API.Settings.Interfaces.Rest.Resources;

public record SettingsRequestResource : IValidatableObject
{
    public int? Id { get; init; }

    [Required]
    [RegularExpression("^global$")]
    public required string Key { get; init; }

    [Required]
    public required ProfileSettingsResource Profile { get; init; }

    [Required]
    public required GeneralPreferencesResource General { get; init; }

    [Required]
    public required NotificationSettingsResource Notifications { get; init; }

    [Required]
    public required SecuritySettingsResource Security { get; init; }

    [Required]
    public required List<MonitoringSensorResource> Sensors { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Sensors is null)
        {
            yield return new ValidationResult("Sensors are required.", [nameof(Sensors)]);
            yield break;
        }

        if (Sensors.Any(sensor => sensor is null))
        {
            yield return new ValidationResult("Sensors cannot contain null entries.", [nameof(Sensors)]);
            yield break;
        }

        if (Sensors.Any(sensor => sensor.Id == Guid.Empty))
            yield return new ValidationResult("Every sensor must have a non-empty ID.", [nameof(Sensors)]);

        if (Sensors.Select(sensor => sensor.Id).Distinct().Count() != Sensors.Count)
            yield return new ValidationResult("Sensor IDs must be unique.", [nameof(Sensors)]);
    }
}

public record ProfileSettingsResource
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    [StringLength(254, MinimumLength = 1)]
    public required string Email { get; init; }

    [StringLength(100)]
    public string Role { get; init; } = string.Empty;
}

public record GeneralPreferencesResource
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Region { get; init; }

    [Required]
    [RegularExpression("^(Celsius|Fahrenheit)$")]
    public required string TemperatureUnit { get; init; }
}

public record NotificationSettingsResource
{
    public bool Email { get; init; }
    public bool InApp { get; init; }
    public bool DailySummary { get; init; }
    public bool WeeklyReport { get; init; }
    public bool InstantAlerts { get; init; }

    [Required]
    [RegularExpression(@"^\d{1,5}(\.\d{1,2})?$")]
    public required string WarningThreshold { get; init; }

    [Required]
    [RegularExpression(@"^\d{1,5}(\.\d{1,2})?$")]
    public required string CriticalThreshold { get; init; }
}

public record SecuritySettingsResource
{
    public bool TwoFactorEnabled { get; init; }
}

public record MonitoringSensorResource
{
    public Guid Id { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Name { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Model { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Serial { get; init; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Type { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Location { get; init; }

    [Required]
    [RegularExpression("^(active|inactive|calibration)$")]
    public required string Status { get; init; }
}
