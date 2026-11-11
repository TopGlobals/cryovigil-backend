using CryoVigil.API.Profiles.Application.QueryServices;
using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using CryoVigil.API.Profiles.Domain.Model.Queries;
using CryoVigil.API.Profiles.Domain.Repositories;

namespace CryoVigil.API.Profiles.Application.Internal.QueryServices;

public class UserProfileQueryService(IUserProfileRepository profileRepository) : IUserProfileQueryService
{
    public async Task<UserProfile> Handle(
        GetCurrentProfileQuery query,
        CancellationToken cancellationToken)
    {
        return await profileRepository.FindCurrentAsNoTrackingAsync(cancellationToken)
               ?? UserProfile.CreateDefault();
    }
}
