using CryoVigil.API.OperationEvents.Application.QueryServices;
using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.OperationEvents.Domain.Model.Queries;
using CryoVigil.API.OperationEvents.Domain.Repositories;

namespace CryoVigil.API.OperationEvents.Application.Internal.QueryServices;

public class HistoryEventQueryService(IHistoryEventRepository historyEventRepository) : IHistoryEventQueryService
{
    public async Task<IEnumerable<HistoryEvent>> Handle(
        GetAllHistoryEventsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await historyEventRepository.ListAsync(cancellationToken);
    }

    public async Task<HistoryEvent?> Handle(
        GetHistoryEventByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await historyEventRepository.FindByIdAsync(query.Id, cancellationToken);
    }
}
