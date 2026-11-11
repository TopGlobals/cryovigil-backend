using CryoVigil.API.Shared.Domain.Model.ValueObjects;

namespace CryoVigil.API.Alerts.Domain.Model.ValueObjects;

/// <summary>
/// Strongly-typed identifier for the <see cref="CryoVigil.API.Alerts.Domain.Model.Aggregates.Alert"/> aggregate root.
/// </summary>
public readonly record struct AlertId(int Value) : IEntityId<int>
{
    public static implicit operator int(AlertId id) => id.Value;
    public static implicit operator AlertId(int value) => new(value);
}
