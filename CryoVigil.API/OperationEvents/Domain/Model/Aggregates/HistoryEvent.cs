using CryoVigil.API.Shared.Domain.Model.Aggregates.Internal;

namespace CryoVigil.API.OperationEvents.Domain.Model.Aggregates;

public class HistoryEvent : AggregateRoot
{
    public int Id { get; private set; }
    public string TitleKey { get; private set; }
    public string Type { get; private set; }
    public string MessageKey { get; private set; }
    public string Severity { get; private set; }
    public string Location { get; private set; }
    public string Sensor { get; private set; }
    public bool IsResolved { get; private set; }
    public List<string> ActionsTakenKeys { get; private set; } = [];
    public DateTime Timestamp { get; private set; }

    protected HistoryEvent()
    {
        TitleKey = string.Empty;
        Type = string.Empty;
        MessageKey = string.Empty;
        Severity = string.Empty;
        Location = string.Empty;
        Sensor = string.Empty;
        Timestamp = DateTime.UtcNow;
    }

    public HistoryEvent(
        string titleKey,
        string type,
        string messageKey,
        string severity,
        string location,
        string sensor,
        bool isResolved,
        IEnumerable<string>? actionsTakenKeys = null,
        DateTime? timestamp = null)
    {
        TitleKey = string.IsNullOrWhiteSpace(titleKey) ? string.Empty : titleKey.Trim();
        Type = string.IsNullOrWhiteSpace(type) ? "Alert" : type.Trim();
        MessageKey = string.IsNullOrWhiteSpace(messageKey) ? string.Empty : messageKey.Trim();
        Severity = string.IsNullOrWhiteSpace(severity) ? "Info" : severity.Trim();
        Location = string.IsNullOrWhiteSpace(location) ? string.Empty : location.Trim();
        Sensor = string.IsNullOrWhiteSpace(sensor) ? string.Empty : sensor.Trim();
        IsResolved = isResolved;
        ActionsTakenKeys = actionsTakenKeys != null ? new List<string>(actionsTakenKeys) : [];
        Timestamp = timestamp ?? DateTime.UtcNow;
    }

    public void MarkAsResolved()
    {
        IsResolved = true;
    }

    public void AddAction(string actionKey)
    {
        if (!string.IsNullOrWhiteSpace(actionKey))
        {
            ActionsTakenKeys.Add(actionKey.Trim());
        }
    }
}
