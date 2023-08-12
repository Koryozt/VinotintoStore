using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Orders.Commands.Create;

internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Cost)
            .NotEmpty()
            .GreaterThan(Amount.MinValue)
            .LessThanOrEqualTo(Amount.MaxValue);

        RuleFor(x => x.Method)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(LongText.MaxLength);

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}
