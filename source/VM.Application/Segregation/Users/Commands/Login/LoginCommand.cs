using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Users.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<string>;