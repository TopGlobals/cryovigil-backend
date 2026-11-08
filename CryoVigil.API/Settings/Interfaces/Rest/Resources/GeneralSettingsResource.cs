using System.Text.Json.Serialization;
using CryoVigil.API.Settings.Domain.Model.ValueObjects;

namespace CryoVigil.API.Settings.Interfaces.Rest.Resources;

public record GeneralSettingsResource(
    string Region,
    [property: JsonConverter(typeof(JsonStringEnumConverter<TemperatureUnit>))]
    TemperatureUnit TemperatureUnit);
