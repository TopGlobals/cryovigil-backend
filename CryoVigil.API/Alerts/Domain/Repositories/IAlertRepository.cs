using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Alerts.Domain.Repositories;

/// <summary>
/// Repository interface for the <see cref="Alert"/> aggregate root.
/// </summary>
public interface IAlertRepository : IBaseRepository<Alert>
{
    /// <summary>
    /// Finds an alert by its primary key, eagerly loading corrective actions and readings.
    /// </summary>
    Task<Alert?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all alerts eagerly loading corrective actions and readings, ordered by raised date descending.
    /// </summary>
    Task<IEnumerable<Alert>> ListWithDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds an alert by its unique code (e.g., ALT-20261005-001).
    /// </summary>
    Task<Alert?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether an alert with the given code already exists.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
