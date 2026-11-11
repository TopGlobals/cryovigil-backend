using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using CryoVigil.API.Profiles.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserProfileRepository(AppDbContext context) : IUserProfileRepository
{
    public async Task<UserProfile?> FindCurrentAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<UserProfile>()
            .FindAsync(new object[] { UserProfile.CurrentProfileId }, cancellationToken);
    }

    public async Task<UserProfile?> FindCurrentAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<UserProfile>()
            .AsNoTracking()
            .SingleOrDefaultAsync(profile => profile.Id == UserProfile.CurrentProfileId, cancellationToken);
    }

    public async Task AddAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        await context.Set<UserProfile>().AddAsync(profile, cancellationToken);
    }
}
