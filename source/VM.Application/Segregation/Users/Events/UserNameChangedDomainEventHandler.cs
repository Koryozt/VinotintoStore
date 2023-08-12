using VM.Application.Abstract;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Abstractions;
using VM.Domain.DomainEvents;
using VM.Domain.Entities;

namespace VM.Application.Members.Events;

internal sealed class UserNameChangedDomainEventHandler
     : IDomainEventHandler<UserNameChangedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserNameChangedDomainEventHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        UserNameChangedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(
            notification.UserId,
            cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendUpdatedInformationEmailAsync(
            user, 
            cancellationToken);
    }
}
