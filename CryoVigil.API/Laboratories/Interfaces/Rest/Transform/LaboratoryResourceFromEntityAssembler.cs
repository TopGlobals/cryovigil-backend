using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Laboratories.Interfaces.Rest.Resources;

namespace CryoVigil.API.Laboratories.Interfaces.Rest.Transform;

public static class LaboratoryResourceFromEntityAssembler
{
    public static LaboratoryResource ToResource(Laboratory laboratory)
    {
        return new LaboratoryResource(
            laboratory.Id,
            laboratory.Name,
            laboratory.Code,
            laboratory.Building,
            laboratory.Floor,
            laboratory.Room,
            ToCamelCase(laboratory.Type),
            laboratory.Description,
            laboratory.TemperatureMin,
            laboratory.TemperatureMax,
            laboratory.Co2Max,
            ToCamelCase(laboratory.GasSensitivity),
            laboratory.VibrationMax,
            ToCamelCase(laboratory.Escalation),
            laboratory.Sensors,
            laboratory.Notifications,
            ToCamelCase(laboratory.Status),
            laboratory.Temperature,
            laboratory.AirQuality,
            laboratory.Unknown,
            laboratory.UpdatedAt);
    }

    private static string ToCamelCase<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var name = value.ToString();
        return $"{char.ToLowerInvariant(name[0])}{name[1..]}";
    }
}
