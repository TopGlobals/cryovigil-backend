using CryoVigil.API.Shared.Domain.Model.Entities;

namespace CryoVigil.API.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}