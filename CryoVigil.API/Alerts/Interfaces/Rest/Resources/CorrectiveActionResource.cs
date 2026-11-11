using System.ComponentModel.DataAnnotations;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Resources;

public record CorrectiveActionResource(
    [Required]
    [StringLength(500, MinimumLength = 10)]
    string Description,

    [Required]
    string TechnicianName,

    DateTime? TakenAt);
