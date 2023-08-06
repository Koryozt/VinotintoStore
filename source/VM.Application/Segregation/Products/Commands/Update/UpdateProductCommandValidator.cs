using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Products.Commands.Update;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(LongText.MaxLength);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(Amount.MinValue)
            .LessThanOrEqualTo(Amount.MaxValue);

        RuleFor(x => x.Stock)
            .NotEmpty()
            .GreaterThanOrEqualTo(Quantity.MinValue)
            .LessThanOrEqualTo(Quantity.MaxValue);
    }
}
