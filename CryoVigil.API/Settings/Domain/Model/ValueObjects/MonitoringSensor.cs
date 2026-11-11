namespace CryoVigil.API.Settings.Domain.Model.ValueObjects;

public class MonitoringSensor
{
    private MonitoringSensor()
    {
        Name = string.Empty;
        Model = string.Empty;
        Serial = string.Empty;
        Type = string.Empty;
        Location = string.Empty;
        Status = string.Empty;
    }

    public MonitoringSensor(
        Guid id,
        string name,
        string model,
        string serial,
        string type,
        string location,
        string status)
    {
        Id = id;
        Name = string.Empty;
        Model = string.Empty;
        Serial = string.Empty;
        Type = string.Empty;
        Location = string.Empty;
        Status = string.Empty;
        Update(name, model, serial, type, location, status);
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Model { get; private set; }
    public string Serial { get; private set; }
    public string Type { get; private set; }
    public string Location { get; private set; }
    public string Status { get; private set; }

    public void Update(string name, string model, string serial, string type, string location, string status)
    {
        if (Id == Guid.Empty)
            throw new ArgumentException("Sensor ID is required.", nameof(Id));

        Name = RequiredText(name, nameof(name), 100);
        Model = RequiredText(model, nameof(model), 100);
        Serial = RequiredText(serial, nameof(serial), 100);
        Type = RequiredText(type, nameof(type), 50);
        Location = RequiredText(location, nameof(location), 100);
        Status = RequiredText(status, nameof(status), 20);
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
