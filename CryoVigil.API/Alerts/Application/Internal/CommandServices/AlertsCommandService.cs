using CryoVigil.API.Alerts.Application.CommandServices;
using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Commands;
using CryoVigil.API.Alerts.Domain.Model.Errors;
using CryoVigil.API.Alerts.Domain.Repositories;
using CryoVigil.API.Shared.Application.Model;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Alerts.Application.Internal.CommandServices;

public class AlertsCommandService(
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork) : IAlertsCommandService
{
    public async Task<Result<Alert>> Handle(
        CreateAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        var code = string.IsNullOrWhiteSpace(command.Code)
            ? $"ALT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}"
            : command.Code.Trim();

        if (await alertRepository.ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<Alert>.Failure(
                AlertErrors.DuplicateAlertCodeWithCode(code),
                AlertError.DuplicateAlertCode);
        }

        try
        {
            var alert = new Alert(
                code,
                command.Title,
                command.Message,
                command.Severity,
                command.Status,
                command.SourceType,
                command.LaboratoryName,
                command.StorageUnitName,
                command.SensorCode,
                command.ReadingCelsius,
                command.SafeMinCelsius,
                command.SafeMaxCelsius,
                command.RaisedAt,
                command.AcknowledgedAt,
                command.AcknowledgedBy,
                command.ResolvedAt,
                command.ResolvedBy,
                command.CorrectiveActions,
                command.Readings);

            await alertRepository.AddAsync(alert, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Alert>.Success(alert);
        }
        catch (ArgumentException ex)
        {
            return Result<Alert>.Failure(AlertError.InvalidLifecycleTransition, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result<Alert>.Failure(AlertError.InvalidLifecycleTransition, ex.Message);
        }
    }

    public async Task<Result<Alert>> Handle(
        UpdateAlertCommand command,
        CancellationToken cancellationToken = default)
    {
        var alert = await alertRepository.FindByIdWithDetailsAsync(command.Id, cancellationToken);
        if (alert is null)
        {
            return Result<Alert>.Failure(
                AlertErrors.AlertNotFound,
                AlertError.AlertNotFound);
        }

        try
        {
            alert.Update(
                command.Title,
                command.Message,
                command.Severity,
                command.Status,
                command.SourceType,
                command.LaboratoryName,
                command.StorageUnitName,
                command.SensorCode,
                command.ReadingCelsius,
                command.SafeMinCelsius,
                command.SafeMaxCelsius,
                command.AcknowledgedAt,
                command.AcknowledgedBy,
                command.ResolvedAt,
                command.ResolvedBy,
                command.CorrectiveActions,
                command.Readings);

            alertRepository.Update(alert);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Alert>.Success(alert);
        }
        catch (ArgumentException ex)
        {
            return Result<Alert>.Failure(AlertError.InvalidLifecycleTransition, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result<Alert>.Failure(AlertError.InvalidLifecycleTransition, ex.Message);
        }
    }
}
