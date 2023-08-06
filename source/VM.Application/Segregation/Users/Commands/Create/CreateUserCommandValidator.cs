using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;
using VM.Domain.ValueObjects.Users;

namespace VM.Application.Segregation.Users.Commands.Create;

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(Email.MaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(Password.MinValue)
            .MaximumLength(Password.MaxValue);
    }
}
