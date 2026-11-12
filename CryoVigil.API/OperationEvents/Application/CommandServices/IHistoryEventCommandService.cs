using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.OperationEvents.Domain.Model.Commands;


namespace CryoVigil.API.OperationEvents.Application.CommandServices;

public interface IHistoryEventCommandService
{
    Task<HistoryEvent?> Handle(CreateHistoryEventCommand command, CancellationToken cancellationToken = default);
    Task<bool> Handle(DeleteHistoryEventCommand command, CancellationToken cancellationToken = default);
}
