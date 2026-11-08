using CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

namespace CryoVigil.API.Laboratories.Domain.Model.Aggregates;

public class Laboratory
{
    private Laboratory()
    {
        Name = string.Empty;
        Code = string.Empty;
        Building = string.Empty;
        Floor = string.Empty;
        Room = string.Empty;
        Description = string.Empty;
        Sensors = new LaboratorySensors();
        Notifications = new LaboratoryNotifications();
    }

    public Laboratory(LaboratoryDetails details)
    {
        Name = string.Empty;
        Code = string.Empty;
        Building = string.Empty;
        Floor = string.Empty;
        Room = string.Empty;
        Description = string.Empty;
        Sensors = new LaboratorySensors();
        Notifications = new LaboratoryNotifications();
        Status = LaboratoryStatus.Pending;
        UpdateDetails(details);
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Building { get; private set; }
    public string Floor { get; private set; }
    public string Room { get; private set; }
    public LaboratoryType Type { get; private set; }
    public string Description { get; private set; }
    public decimal TemperatureMin { get; private set; }
    public decimal TemperatureMax { get; private set; }
    public int Co2Max { get; private set; }
    public GasSensitivity GasSensitivity { get; private set; }
    public decimal VibrationMax { get; private set; }
    public LaboratoryEscalation Escalation { get; private set; }
    public LaboratorySensors Sensors { get; private set; }
    public LaboratoryNotifications Notifications { get; private set; }
    public LaboratoryStatus Status { get; private set; }
    public decimal? Temperature { get; private set; }
    public int? AirQuality { get; private set; }
    public int? Unknown { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public void UpdateDetails(LaboratoryDetails details)
    {
        ArgumentNullException.ThrowIfNull(details);

        var name = RequiredText(details.Name, nameof(details.Name), 100);
        var code = RequiredText(details.Code, nameof(details.Code), 50);
        var building = OptionalText(details.Building, nameof(details.Building), 50);
        var floor = OptionalText(details.Floor, nameof(details.Floor), 20);
        var room = OptionalText(details.Room, nameof(details.Room), 50);
        var description = OptionalText(details.Description, nameof(details.Description), 500);

        if (!Enum.IsDefined(details.Type))
            throw new ArgumentOutOfRangeException(nameof(details.Type));
        if (!Enum.IsDefined(details.GasSensitivity))
            throw new ArgumentOutOfRangeException(nameof(details.GasSensitivity));
        if (!Enum.IsDefined(details.Escalation))
            throw new ArgumentOutOfRangeException(nameof(details.Escalation));
        if (details.TemperatureMin >= details.TemperatureMax)
            throw new ArgumentException("Minimum temperature must be less than maximum temperature.");
        if (details.Co2Max <= 0)
            throw new ArgumentOutOfRangeException(nameof(details.Co2Max), "CO2 maximum must be greater than zero.");
        if (details.VibrationMax < 0)
            throw new ArgumentOutOfRangeException(nameof(details.VibrationMax), "Vibration maximum cannot be negative.");

        Name = name;
        Code = code;
        Building = building;
        Floor = floor;
        Room = room;
        Description = description;
        Type = details.Type;
        TemperatureMin = details.TemperatureMin;
        TemperatureMax = details.TemperatureMax;
        Co2Max = details.Co2Max;
        GasSensitivity = details.GasSensitivity;
        VibrationMax = details.VibrationMax;
        Escalation = details.Escalation;
        Sensors = details.Sensors ?? new LaboratorySensors();
        Notifications = details.Notifications ?? new LaboratoryNotifications();
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
