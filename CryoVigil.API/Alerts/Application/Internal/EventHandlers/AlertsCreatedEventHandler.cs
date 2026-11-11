using CryoVigil.API.Alerts.Domain.Model.Events;
using CryoVigil.API.Shared.Application.Internal.EventHandlers;
using Microsoft.Extensions.Logging;

namespace CryoVigil.API.Alerts.Application.Internal.EventHandlers;

public class AlertCreatedEventHandler(ILogger<AlertCreatedEventHandler> logger) : IEventHandler<AlertCreatedEvent>
{
    public Task Handle(AlertCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: Alert '{Code}' (ID: {Id}) was created.", notification.Code, notification.AlertId.Value);
        return Task.CompletedTask;
    }
}
