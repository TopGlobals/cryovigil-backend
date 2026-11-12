using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.OperationEvents.Domain.Repositories;

public interface IHistoryEventRepository : IBaseRepository<HistoryEvent>
{
}
