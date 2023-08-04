using VM.Domain.Primitives;

namespace VM.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
