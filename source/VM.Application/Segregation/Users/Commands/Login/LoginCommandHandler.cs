using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace VM.Application.Segregation.Users.Commands.Login;

internal class LoginCommandHandler : AbstractValidator<LoginCommand>
{
    public LoginCommandHandler()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
