using CryoVigil.API.History.Application.QueryServices;
using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Domain.Model.Queries;
using CryoVigil.API.History.Domain.Repositories;

namespace CryoVigil.API.History.Application.Internal.QueryServices;

public class HistoryEventQueryService : IHistoryEventQueryService
{
    private readonly IHistoryEventRepository _historyEventRepository;

    public HistoryEventQueryService(IHistoryEventRepository historyEventRepository)
    {
        _historyEventRepository = historyEventRepository;
    }

    public async Task<IEnumerable<HistoryEvent>> Handle(GetAllHistoryEventsQuery query)
    {
        return await _historyEventRepository.ListAsync();
    }

    public async Task<HistoryEvent?> Handle(GetHistoryEventByIdQuery query)
    {
        return await _historyEventRepository.FindByIdAsync(query.Id);
    }
}