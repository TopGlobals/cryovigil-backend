using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.OperationEvents.Domain.Model.Queries;

namespace CryoVigil.API.OperationEvents.Application.QueryServices;

public interface IHistoryEventQueryService
{
    Task<IEnumerable<HistoryEvent>> Handle(GetAllHistoryEventsQuery query, CancellationToken cancellationToken = default);
    Task<HistoryEvent?> Handle(GetHistoryEventByIdQuery query, CancellationToken cancellationToken = default);
}
