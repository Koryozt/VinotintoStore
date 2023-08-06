using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Commands.Create;

internal class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(Code.MinLength)
            .MaximumLength(Code.MaxLength);

        RuleFor(x => x.Discount)
            .NotEmpty()
            .GreaterThan(Amount.MinValue)
            .LessThanOrEqualTo(100.00);

        RuleFor(x => x.ExpirationDate)
            .NotEmpty()
            .GreaterThan(DateTime.UtcNow);

        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}
