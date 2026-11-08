namespace CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

public record LaboratoryDetails(
    string Name,
    string Code,
    LaboratoryType Type,
    string Building = "",
    string Floor = "",
    string Room = "",
    string Description = "",
    decimal TemperatureMin = 15,
    decimal TemperatureMax = 30,
    int Co2Max = 1000,
    GasSensitivity GasSensitivity = GasSensitivity.LowGeneral,
    decimal VibrationMax = 5,
    LaboratoryEscalation Escalation = LaboratoryEscalation.StopActivity,
    LaboratorySensors? Sensors = null,
    LaboratoryNotifications? Notifications = null);
