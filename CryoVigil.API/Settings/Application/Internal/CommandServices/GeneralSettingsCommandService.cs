using CryoVigil.API.Settings.Application.CommandServices;
using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Commands;
using CryoVigil.API.Settings.Domain.Repositories;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Settings.Application.Internal.CommandServices;

public class GeneralSettingsCommandService(
    IGeneralSettingsRepository generalSettingsRepository,
    IUnitOfWork unitOfWork) : IGeneralSettingsCommandService
{
    public async Task<GeneralSettings> Handle(
        UpdateGeneralSettingsCommand command,
        CancellationToken cancellationToken)
    {
        var settings = await generalSettingsRepository.FindGlobalAsync(cancellationToken);
        if (settings is null)
        {
            settings = new GeneralSettings(command.Region, command.TemperatureUnit);
            await generalSettingsRepository.AddAsync(settings, cancellationToken);
        }
        else
        {
            settings.Update(command.Region, command.TemperatureUnit);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return settings;
    }
}
