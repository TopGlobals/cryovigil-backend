using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Commands;
using CryoVigil.API.Shared.Application.Model;

namespace CryoVigil.API.Alerts.Application.CommandServices;

public interface IAlertsCommandService
{
    Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken = default);

    Task<Result<Alert>> Handle(UpdateAlertCommand command, CancellationToken cancellationToken = default);
}
