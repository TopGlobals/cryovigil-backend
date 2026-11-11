namespace CryoVigil.API.Profiles.Domain.Model.Commands;

public record UpdateCurrentProfileCommand(string Name, string Email, string Role);
