using System.ComponentModel.DataAnnotations;

namespace CryoVigil.API.History.Interfaces.Rest.Resources;

public record CreateHistoryEventResource(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string TitleKey,

    [Required]
    [StringLength(50)]
    string Type,

    [Required]
    [StringLength(500, MinimumLength = 5)]
    string MessageKey,

    [Required]
    [StringLength(30)]
    string Severity,

    [Required]
    [StringLength(100)]
    string Location,

    [Required]
    [StringLength(50)]
    string Sensor,

    bool IsResolved,

    IEnumerable<string>? ActionsTakenKeys,

    DateTime? Timestamp
);
