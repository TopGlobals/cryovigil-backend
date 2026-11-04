using Cortex.Mediator.Notifications;
using CryoVigil.API.Shared.Domain.Model.Events;

namespace CryoVigil.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}