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
        if (await laboratoryRepository.ExistsByCodeAsync(
                laboratory.Code,
                cancellationToken: cancellationToken))
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

    public async Task<Result<Laboratory>> Handle(
        UpdateLaboratoryCommand command,
        CancellationToken cancellationToken)
    {
        var laboratory = await laboratoryRepository.FindByIdAsync(command.Id, cancellationToken);
        if (laboratory is null)
        {
            return Result<Laboratory>.Failure(
                Error.NotFound(
                    "Laboratories.NotFound",
                    $"Laboratory with ID '{command.Id}' was not found."));
        }

        var updatedLaboratory = new Laboratory(command.Details);
        if (await laboratoryRepository.ExistsByCodeAsync(
                updatedLaboratory.Code,
                command.Id,
                cancellationToken))
        {
            return Result<Laboratory>.Failure(
                Error.Conflict(
                    "Laboratories.DuplicateCode",
                    $"A laboratory with code '{updatedLaboratory.Code}' already exists."));
        }

        laboratory.UpdateDetails(command.Details);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<Laboratory>.Success(laboratory);
    }

    public async Task<Result> Handle(
        DeleteLaboratoryCommand command,
        CancellationToken cancellationToken)
    {
        var laboratory = await laboratoryRepository.FindByIdAsync(command.Id, cancellationToken);
        if (laboratory is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Laboratories.NotFound",
                    $"Laboratory with ID '{command.Id}' was not found."));
        }

        laboratoryRepository.Remove(laboratory);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
