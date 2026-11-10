using CryoVigil.API.History.Application.CommandServices;
using CryoVigil.API.History.Domain.Model.Aggregates;
using CryoVigil.API.History.Domain.Model.Commands;
using CryoVigil.API.History.Domain.Repositories;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.History.Application.Internal.CommandServices;

public class HistoryEventCommandService : IHistoryEventCommandService
{
    private readonly IHistoryEventRepository _historyEventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HistoryEventCommandService(IHistoryEventRepository historyEventRepository, IUnitOfWork unitOfWork)
    {
        _historyEventRepository = historyEventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<HistoryEvent?> Handle(CreateHistoryEventCommand command)
    {
        var historyEvent = new HistoryEvent(
            command.EventCode,
            command.EventTime,
            command.Severity,
            command.SourceModule,
            command.UserEmail,
            command.EventAction,
            command.Message
        );

        await _historyEventRepository.AddAsync(historyEvent);
        await _unitOfWork.CompleteAsync();

        return historyEvent;
    }
}