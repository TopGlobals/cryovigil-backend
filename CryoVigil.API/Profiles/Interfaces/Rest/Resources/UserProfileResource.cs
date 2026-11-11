using System.ComponentModel.DataAnnotations;

namespace CryoVigil.API.Profiles.Interfaces.Rest.Resources;

public record UserProfileResource
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
