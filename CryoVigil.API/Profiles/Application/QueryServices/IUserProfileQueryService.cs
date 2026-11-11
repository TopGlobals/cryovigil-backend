using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using CryoVigil.API.Profiles.Domain.Model.Queries;

namespace CryoVigil.API.Profiles.Application.QueryServices;

public interface IUserProfileQueryService
{
    Task<UserProfile> Handle(GetCurrentProfileQuery query, CancellationToken cancellationToken);
}
