using VM.Domain.Primitives;
using MediatR;

namespace VM.Application.Abstractions.Messaging;

# pragma warning disable

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
