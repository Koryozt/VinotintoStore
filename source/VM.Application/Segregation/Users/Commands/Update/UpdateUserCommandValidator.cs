using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Users.Commands.Update;

internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);
    }
}
