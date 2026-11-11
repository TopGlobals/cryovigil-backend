using CryoVigil.API.Profiles.Application.CommandServices;
using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using CryoVigil.API.Profiles.Domain.Model.Commands;
using CryoVigil.API.Profiles.Domain.Repositories;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Profiles.Application.Internal.CommandServices;

public class UserProfileCommandService(
    IUserProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IUserProfileCommandService
{
    public async Task<UserProfile> Handle(
        UpdateCurrentProfileCommand command,
        CancellationToken cancellationToken)
    {
        var profile = await profileRepository.FindCurrentAsync(cancellationToken);
        if (profile is null)
        {
            profile = UserProfile.CreateDefault();
            profile.Update(command.Name, command.Email, command.Role);
            await profileRepository.AddAsync(profile, cancellationToken);
        }
        else
        {
            profile.Update(command.Name, command.Email, command.Role);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return profile;
    }
}
