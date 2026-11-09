using CryoVigil.API.Alerts.Domain.Model.ValueObjects;

namespace CryoVigil.API.Alerts.Domain.Model.Entities;

/// <summary>
/// Corrective action registered by laboratory staff to resolve an alert.
/// Forms part of the audit trail for regulatory compliance.
/// </summary>
public class CorrectiveAction
{
    public const int MinDescriptionLength = 10;
    public const int MaxDescriptionLength = 500;

    public int Id { get; private set; }
    public AlertId AlertId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string TechnicianName { get; private set; } = string.Empty;
    public DateTime TakenAt { get; private set; }

    protected CorrectiveAction() { }

    public CorrectiveAction(string description, string technicianName, DateTime takenAt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(technicianName);

        var trimmed = description.Trim();
        if (trimmed.Length < MinDescriptionLength || trimmed.Length > MaxDescriptionLength)
            throw new ArgumentException(
                $"Description must be between {MinDescriptionLength} and {MaxDescriptionLength} characters.",
                nameof(description));

        Description = trimmed;
        TechnicianName = technicianName.Trim();
        TakenAt = takenAt;
    }
}
