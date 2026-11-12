using CryoVigil.API.OperationEvents.Application.CommandServices;
using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.OperationEvents.Domain.Model.Commands;
using CryoVigil.API.OperationEvents.Domain.Repositories;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.OperationEvents.Application.Internal.CommandServices;

public class HistoryEventCommandService(
    IHistoryEventRepository historyEventRepository,
    IUnitOfWork unitOfWork) : IHistoryEventCommandService
{
    public async Task<HistoryEvent?> Handle(
        CreateHistoryEventCommand command,
        CancellationToken cancellationToken = default)
    {
        var historyEvent = new HistoryEvent(
            command.TitleKey,
            command.Type,
            command.MessageKey,
            command.Severity,
            command.Location,
            command.Sensor,
            command.IsResolved,
            command.ActionsTakenKeys,
            command.Timestamp
        );

        await historyEventRepository.AddAsync(historyEvent, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return historyEvent;
    }

    public async Task<bool> Handle(
        DeleteHistoryEventCommand command,
        CancellationToken cancellationToken = default)
    {
        var historyEvent = await historyEventRepository.FindByIdAsync(command.Id, cancellationToken);

        if (historyEvent == null)
            return false;

        historyEventRepository.Remove(historyEvent);
        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}