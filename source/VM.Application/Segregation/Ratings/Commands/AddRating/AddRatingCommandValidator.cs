using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Ratings.Commands.AddRating;

internal class AddRatingCommandValidator : AbstractValidator<AddRatingCommand>
{
    public AddRatingCommandValidator()
    {
        RuleFor(x => x.Score)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(LongText.MaxLength);

        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}
