using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Laboratories.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Laboratories.Infrastructure.Persistence.Efc.Repositories;

public class LaboratoryRepository(AppDbContext context)
    : BaseRepository<Laboratory>(context), ILaboratoryRepository
{
    public async Task<bool> ExistsByCodeAsync(
        string code,
        int? excludingLaboratoryId = null,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Laboratory>()
            .AnyAsync(
                laboratory => laboratory.Code == code &&
                              (!excludingLaboratoryId.HasValue || laboratory.Id != excludingLaboratoryId.Value),
                cancellationToken);
    }
}
