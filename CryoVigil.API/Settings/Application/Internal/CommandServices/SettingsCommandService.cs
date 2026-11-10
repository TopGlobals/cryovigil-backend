using CryoVigil.API.Settings.Application.CommandServices;
using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Commands;
using CryoVigil.API.Settings.Domain.Repositories;
using CryoVigil.API.Shared.Application.Model;
using CryoVigil.API.Shared.Domain.Model.Errors;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Settings.Application.Internal.CommandServices;

public class SettingsCommandService(
    IGeneralSettingsRepository settingsRepository,
    IUnitOfWork unitOfWork) : ISettingsCommandService
{
    public async Task<Result<GeneralSettings>> Handle(
        CreateSettingsCommand command,
        CancellationToken cancellationToken)
    {
        if (await settingsRepository.FindGlobalAsync(cancellationToken) is not null)
        {
            return Result<GeneralSettings>.Failure(
                Error.Conflict(
                    "Settings.AlreadyExists",
                    "Global settings already exist."));
        }

        var settings = GeneralSettings.CreateDefault();
        settings.UpdateSettings(command.Details);
        await settingsRepository.AddAsync(settings, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<GeneralSettings>.Success(settings);
    }

    public async Task<Result<GeneralSettings>> Handle(
        UpdateSettingsCommand command,
        CancellationToken cancellationToken)
    {
        var settings = await settingsRepository.FindByIdAsync(command.Id, cancellationToken);
        if (settings is null)
        {
            return Result<GeneralSettings>.Failure(
                Error.NotFound(
                    "Settings.NotFound",
                    $"Settings with ID '{command.Id}' were not found."));
        }

        settings.UpdateSettings(command.Details);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<GeneralSettings>.Success(settings);
    }

    public async Task<Result> Handle(
        DeleteSettingsCommand command,
        CancellationToken cancellationToken)
    {
        var settings = await settingsRepository.FindByIdAsync(command.Id, cancellationToken);
        if (settings is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Settings.NotFound",
                    $"Settings with ID '{command.Id}' were not found."));
        }

        settingsRepository.Remove(settings);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
