using System.ComponentModel.DataAnnotations;

namespace CryoVigil.API.Settings.Interfaces.Rest.Resources;

public record UpdateGeneralSettingsResource
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Region { get; init; }

    [Required]
    [RegularExpression("^(Celsius|Fahrenheit)$")]
    public required string TemperatureUnit { get; init; }
}
