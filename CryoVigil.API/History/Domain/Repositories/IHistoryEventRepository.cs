using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.History.Domain.Repositories;

public interface IHistoryEventRepository : IBaseRepository<HistoryEvent>
{
}