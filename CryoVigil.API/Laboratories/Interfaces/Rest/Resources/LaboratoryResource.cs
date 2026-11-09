using CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

namespace CryoVigil.API.Laboratories.Interfaces.Rest.Resources;

public record LaboratoryResource(
    int Id,
    string Name,
    string Code,
    string Building,
    string Floor,
    string Room,
    string Type,
    string Description,
    decimal TemperatureMin,
    decimal TemperatureMax,
    int Co2Max,
    string GasSensitivity,
    decimal VibrationMax,
    string Escalation,
    LaboratorySensors Sensors,
    LaboratoryNotifications Notifications,
    string Status,
    decimal? Temperature,
    int? AirQuality,
    int? Unknown,
    DateTime? UpdatedAt);
