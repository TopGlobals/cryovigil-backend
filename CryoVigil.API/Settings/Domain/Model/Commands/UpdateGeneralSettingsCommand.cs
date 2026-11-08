using CryoVigil.API.Settings.Domain.Model.ValueObjects;

namespace CryoVigil.API.Settings.Domain.Model.Commands;

public record UpdateGeneralSettingsCommand(string Region, TemperatureUnit TemperatureUnit);
