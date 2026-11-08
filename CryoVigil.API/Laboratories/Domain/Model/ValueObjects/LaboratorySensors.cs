namespace CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

public record LaboratorySensors(
    bool Temperature = true,
    bool AirQuality = true,
    bool AiDetection = true,
    bool AirConditioning = false);
