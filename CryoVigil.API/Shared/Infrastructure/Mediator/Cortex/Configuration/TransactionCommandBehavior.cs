using Cortex.Mediator.Commands;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

public class TransactionCommandBehavior<TCommand>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionCommandBehavior<TCommand>> logger)
    : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    public async Task Handle(
        TCommand command,
        CommandHandlerDelegate next,
        CancellationToken ct)
    {
        await next();
        logger.LogDebug("Committing unit of work for {CommandType}", typeof(TCommand).Name);
        await unitOfWork.CompleteAsync(ct);
    }
}
