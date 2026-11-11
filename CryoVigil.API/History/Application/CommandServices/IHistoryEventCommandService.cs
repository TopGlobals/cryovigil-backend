using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Domain.Model.Commands;

namespace CryoVigil.API.History.Application.CommandServices;

public interface IHistoryEventCommandService
{
    Task<HistoryEvent?> Handle(CreateHistoryEventCommand command, CancellationToken cancellationToken = default);
}
