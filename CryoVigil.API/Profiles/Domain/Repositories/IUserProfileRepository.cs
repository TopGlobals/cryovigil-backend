using CryoVigil.API.Profiles.Domain.Model.Aggregates;

namespace CryoVigil.API.Profiles.Domain.Repositories;

public interface IUserProfileRepository
{
    Task<UserProfile?> FindCurrentAsync(CancellationToken cancellationToken = default);

    Task<UserProfile?> FindCurrentAsNoTrackingAsync(CancellationToken cancellationToken = default);

    Task AddAsync(UserProfile profile, CancellationToken cancellationToken = default);
}
