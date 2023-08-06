using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.ValueObjects.General;
using VM.Domain.ValueObjects.Users;

namespace VM.Application.Segregation.Users.Commands.Create;

public sealed record CreateUserCommand(
    string Firstname,
    string Lastname,
    string Email,
    string Password) : ICommand<Guid>;