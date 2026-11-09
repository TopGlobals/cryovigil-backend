using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Laboratories.Domain.Model.Commands;
using CryoVigil.API.Shared.Application.Model;

namespace CryoVigil.API.Laboratories.Application.CommandServices;

public interface ILaboratoriesCommandService
{
    Task<Result<Laboratory>> Handle(CreateLaboratoryCommand command, CancellationToken cancellationToken);

    Task<Result<Laboratory>> Handle(UpdateLaboratoryCommand command, CancellationToken cancellationToken);

    Task<Result> Handle(DeleteLaboratoryCommand command, CancellationToken cancellationToken);
}
