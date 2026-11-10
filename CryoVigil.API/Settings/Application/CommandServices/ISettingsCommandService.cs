using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Commands;
using CryoVigil.API.Shared.Application.Model;

namespace CryoVigil.API.Settings.Application.CommandServices;

public interface ISettingsCommandService
{
    Task<Result<GeneralSettings>> Handle(CreateSettingsCommand command, CancellationToken cancellationToken);

    Task<Result<GeneralSettings>> Handle(UpdateSettingsCommand command, CancellationToken cancellationToken);

    Task<Result> Handle(DeleteSettingsCommand command, CancellationToken cancellationToken);
}
