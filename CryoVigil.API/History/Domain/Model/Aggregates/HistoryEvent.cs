using CryoVigil.API.Shared.Domain.Model.Aggregates.Internal;

namespace CryoVigil.API.History.Domain.Model.Aggregates;

public class HistoryEvent : AggregateRoot
{
    public string EventCode { get; private set; }
    public DateTime EventTime { get; private set; }
    public string Severity { get; private set; }
    public string SourceModule { get; private set; }
    public string UserEmail { get; private set; }
    public string EventAction { get; private set; }
    public string Message { get; private set; }

    public HistoryEvent()
    {
        EventCode = string.Empty;
        Severity = string.Empty;
        SourceModule = string.Empty;
        UserEmail = string.Empty;
        EventAction = string.Empty;
        Message = string.Empty;
    }

    public HistoryEvent(string eventCode, DateTime eventTime, string severity, string sourceModule, string userEmail, string eventAction, string message)
    {
        EventCode = eventCode;
        EventTime = eventTime;
        Severity = severity;
        SourceModule = sourceModule;
        UserEmail = userEmail;
        EventAction = eventAction;
        Message = message;
    }
}