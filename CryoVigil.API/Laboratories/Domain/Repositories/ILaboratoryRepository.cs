using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Laboratories.Domain.Repositories;

public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    Task<bool> ExistsByCodeAsync(
        string code,
        int? excludingLaboratoryId = null,
        CancellationToken cancellationToken = default);
}
