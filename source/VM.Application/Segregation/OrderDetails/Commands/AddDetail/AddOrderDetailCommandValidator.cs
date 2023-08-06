using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.OrderDetails.Commands.AddDetail;

internal class AddOrderDetailCommandValidator : AbstractValidator<AddOrderDetailCommand>
{
    public AddOrderDetailCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(Quantity.MinValue)
            .LessThanOrEqualTo(Quantity.MaxValue);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(Amount.MinValue)
            .LessThanOrEqualTo(Amount.MaxValue);

        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.OrderId) 
            .NotEmpty();
    }
}
