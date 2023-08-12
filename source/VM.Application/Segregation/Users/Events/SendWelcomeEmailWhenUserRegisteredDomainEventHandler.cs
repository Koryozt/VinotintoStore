using VM.Application.Abstract;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Abstractions;
using VM.Domain.DomainEvents;
using VM.Domain.Entities;

namespace VM.Application.Members.Events;

internal sealed class UserRegisteredDomainEventHandler
     : IDomainEventHandler<UserRegisteredDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserRegisteredDomainEventHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(
            notification.UserId,
            cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendWelcomeEmailAsync(user, cancellationToken);
    }
}
