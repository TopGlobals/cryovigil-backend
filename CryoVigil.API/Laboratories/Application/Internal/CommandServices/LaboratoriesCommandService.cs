using CryoVigil.API.Laboratories.Application.CommandServices;
using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Laboratories.Domain.Model.Commands;
using CryoVigil.API.Laboratories.Domain.Repositories;
using CryoVigil.API.Shared.Application.Model;
using CryoVigil.API.Shared.Domain.Model.Errors;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Laboratories.Application.Internal.CommandServices;

public class LaboratoriesCommandService(
    ILaboratoryRepository laboratoryRepository,
    IUnitOfWork unitOfWork) : ILaboratoriesCommandService
{
    public async Task<Result<Laboratory>> Handle(
        CreateLaboratoryCommand command,
        CancellationToken cancellationToken)
    {
        var laboratory = new Laboratory(command.Details);
        if (await laboratoryRepository.ExistsByCodeAsync(laboratory.Code, cancellationToken))
        {
            return Result<Laboratory>.Failure(
                Error.Conflict(
                    "Laboratories.DuplicateCode",
                    $"A laboratory with code '{laboratory.Code}' already exists."));
        }

        await laboratoryRepository.AddAsync(laboratory, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<Laboratory>.Success(laboratory);
    }
}
