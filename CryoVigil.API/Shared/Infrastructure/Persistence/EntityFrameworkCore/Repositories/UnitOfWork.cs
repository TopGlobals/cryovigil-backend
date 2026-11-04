using CryoVigil.API.Shared.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}