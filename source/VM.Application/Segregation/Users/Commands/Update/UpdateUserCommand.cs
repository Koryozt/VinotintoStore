using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Users.Commands.Update;

public sealed record UpdateUserCommand(
    Guid Id,
    string Firstname,
    string Lastname) : ICommand;