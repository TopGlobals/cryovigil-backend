namespace CryoVigil.API.Settings.Domain.Model.ValueObjects;

public class NotificationSettings
{
    private NotificationSettings()
    {
        WarningThreshold = string.Empty;
        CriticalThreshold = string.Empty;
    }

    public NotificationSettings(
        bool email,
        bool inApp,
        bool dailySummary,
        bool weeklyReport,
        bool instantAlerts,
        string warningThreshold,
        string criticalThreshold)
    {
        WarningThreshold = string.Empty;
        CriticalThreshold = string.Empty;
        Update(email, inApp, dailySummary, weeklyReport, instantAlerts, warningThreshold, criticalThreshold);
    }

    public bool Email { get; private set; }
    public bool InApp { get; private set; }
    public bool DailySummary { get; private set; }
    public bool WeeklyReport { get; private set; }
    public bool InstantAlerts { get; private set; }
    public string WarningThreshold { get; private set; }
    public string CriticalThreshold { get; private set; }

    public void Update(
        bool email,
        bool inApp,
        bool dailySummary,
        bool weeklyReport,
        bool instantAlerts,
        string warningThreshold,
        string criticalThreshold)
    {
        Email = email;
        InApp = inApp;
        DailySummary = dailySummary;
        WeeklyReport = weeklyReport;
        InstantAlerts = instantAlerts;
        WarningThreshold = RequiredText(warningThreshold, nameof(warningThreshold), 10);
        CriticalThreshold = RequiredText(criticalThreshold, nameof(criticalThreshold), 10);
    }

    private static string RequiredText(string value, string parameterName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("A value is required.", parameterName);

        var normalizedValue = value.Trim();
        if (normalizedValue.Length > maxLength)
            throw new ArgumentException($"Value cannot exceed {maxLength} characters.", parameterName);

        return normalizedValue;
    }
}
