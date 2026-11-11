using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using CryoVigil.API.Profiles.Interfaces.Rest.Resources;

namespace CryoVigil.API.Profiles.Interfaces.Rest.Transform;

public static class UserProfileResourceFromEntityAssembler
{
    public static UserProfileResource ToResource(UserProfile profile)
    {
        return new UserProfileResource
        {
            Name = profile.Name,
            Email = profile.Email,
            Role = profile.Role
        };
    }
}
