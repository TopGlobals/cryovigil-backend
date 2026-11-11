using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using CryoVigil.API.Profiles.Domain.Model.Commands;

namespace CryoVigil.API.Profiles.Application.CommandServices;

public interface IUserProfileCommandService
{
    Task<UserProfile> Handle(UpdateCurrentProfileCommand command, CancellationToken cancellationToken);
}
