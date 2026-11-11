namespace CryoVigil.API.Profiles.Domain.Model.Aggregates;

public class UserProfile
{
    public const int CurrentProfileId = 1;

    private UserProfile()
    {
        Name = string.Empty;
        Email = string.Empty;
        Role = string.Empty;
    }

    public UserProfile(string name, string email, string role)
    {
        Id = CurrentProfileId;
        Name = string.Empty;
        Email = string.Empty;
        Role = string.Empty;
        Update(name, email, role);
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }

    public static UserProfile CreateDefault()
    {
        return new UserProfile("Dr. Alex Vance", "alex.vance@topglobals.com", "Head Researcher");
    }

    public void Update(string name, string email, string role)
    {
        Name = RequiredText(name, nameof(name), 100);
        Email = RequiredText(email, nameof(email), 254);
        Role = OptionalText(role, nameof(role), 100);
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

    private static string OptionalText(string? value, string parameterName, int maxLength)
    {
        var normalizedValue = value?.Trim() ?? string.Empty;
        if (normalizedValue.Length > maxLength)
            throw new ArgumentException($"Value cannot exceed {maxLength} characters.", parameterName);

        return normalizedValue;
    }
}
