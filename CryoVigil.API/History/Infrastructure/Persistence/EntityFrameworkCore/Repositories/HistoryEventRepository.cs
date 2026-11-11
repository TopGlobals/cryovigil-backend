using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace CryoVigil.API.History.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class HistoryEventRepository(AppDbContext context)
    : BaseRepository<HistoryEvent>(context), IHistoryEventRepository
{
}
