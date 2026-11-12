using CryoVigil.API.OperationEvents.Domain.Model.Aggregates;
using CryoVigil.API.OperationEvents.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace CryoVigil.API.OperationEvents.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class HistoryEventRepository(AppDbContext context)
    : BaseRepository<HistoryEvent>(context), IHistoryEventRepository
{
}
