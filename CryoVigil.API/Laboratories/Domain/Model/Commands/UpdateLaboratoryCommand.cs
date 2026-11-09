using CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

namespace CryoVigil.API.Laboratories.Domain.Model.Commands;

public record UpdateLaboratoryCommand(int Id, LaboratoryDetails Details);
