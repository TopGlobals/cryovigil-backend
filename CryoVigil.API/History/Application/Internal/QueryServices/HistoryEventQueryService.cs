using CryoVigil.API.History.Application.QueryServices;
using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Domain.Model.Queries;
using CryoVigil.API.History.Domain.Repositories;

namespace CryoVigil.API.History.Application.Internal.QueryServices;

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
