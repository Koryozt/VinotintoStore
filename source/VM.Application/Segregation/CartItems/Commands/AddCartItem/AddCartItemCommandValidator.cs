using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.CartItems.Commands.Create;

internal class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(Quantity.MinValue) 
            .LessThanOrEqualTo(Quantity.MaxValue);

        RuleFor(x => x.TotalPrice)
            .NotEmpty()
            .GreaterThan(Amount.MinValue)
            .LessThanOrEqualTo(Amount.MaxValue);

        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.ShoppingCartId)
            .NotEmpty();
    }
}
