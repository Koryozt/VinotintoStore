using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Application.Segregation.Ratings.Commands.AddRating;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Ratings.Commands.Update;

internal class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
{
    public UpdateRatingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Score)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(LongText.MaxLength);
    }
}
