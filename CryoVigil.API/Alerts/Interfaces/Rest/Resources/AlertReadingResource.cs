using System.ComponentModel.DataAnnotations;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Resources;

public record AlertReadingResource(
    [Required]
    DateTime TakenAt,

    [Required]
    double ValueCelsius);
