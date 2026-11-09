using System.ComponentModel.DataAnnotations;
using CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

namespace CryoVigil.API.Laboratories.Interfaces.Rest.Resources;

public record CreateLaboratoryResource
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
}
