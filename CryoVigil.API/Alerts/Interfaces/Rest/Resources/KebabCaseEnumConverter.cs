using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Resources;

/// <summary>
/// Serializes and deserializes enums using lowercase kebab-case (e.g. ThermalExcursion -> "thermal-excursion"),
/// matching the exact format expected and produced by the frontend application.
/// </summary>
public class KebabCaseEnumConverter<T>() : JsonStringEnumConverter<T>(JsonNamingPolicy.KebabCaseLower)
    where T : struct, Enum;
