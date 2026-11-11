using System.ComponentModel.DataAnnotations;
using CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

namespace CryoVigil.API.Laboratories.Interfaces.Rest.Resources;

public record CreateLaboratoryResource : IValidatableObject
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Name { get; init; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    [RegularExpression(@".*\S.*")]
    public required string Code { get; init; }

    [Required]
    [RegularExpression("^(bioSafety|chemistry|clinical|research)$")]
    public required string Type { get; init; }

    [StringLength(50)]
    public string Building { get; init; } = string.Empty;

    [StringLength(20)]
    public string Floor { get; init; } = string.Empty;

    [StringLength(50)]
    public string Room { get; init; } = string.Empty;

    [StringLength(500)]
    public string Description { get; init; } = string.Empty;

    public decimal TemperatureMin { get; init; } = 15;
    public decimal TemperatureMax { get; init; } = 30;
    public int Co2Max { get; init; } = 1000;

    [RegularExpression("^(lowGeneral|mediumEnhanced|highHazardous)$")]
    public string GasSensitivity { get; init; } = "lowGeneral";

    public decimal VibrationMax { get; init; } = 5;

    [RegularExpression("^(stopActivity|notifySupervisor|logReview)$")]
    public string Escalation { get; init; } = "stopActivity";

    public LaboratorySensors Sensors { get; init; } = new();
    public LaboratoryNotifications Notifications { get; init; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TemperatureMin >= TemperatureMax)
            yield return new ValidationResult(
                "Minimum temperature must be less than maximum temperature.",
                [nameof(TemperatureMin), nameof(TemperatureMax)]);

        if (Co2Max <= 0)
            yield return new ValidationResult("CO2 maximum must be greater than zero.", [nameof(Co2Max)]);

        if (VibrationMax < 0)
            yield return new ValidationResult(
                "Vibration maximum cannot be negative.",
                [nameof(VibrationMax)]);
    }
}
