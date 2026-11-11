using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Domain.Model.Queries;

namespace CryoVigil.API.History.Application.QueryServices;

public interface IHistoryEventQueryService
{
    Task<IEnumerable<HistoryEvent>> Handle(GetAllHistoryEventsQuery query, CancellationToken cancellationToken = default);
    Task<HistoryEvent?> Handle(GetHistoryEventByIdQuery query, CancellationToken cancellationToken = default);
}
