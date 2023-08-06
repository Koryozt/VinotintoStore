using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace VM.Application.Segregation.Categories.Commands.AddProduct;

internal class AddProductToCategoryCommandValidator : AbstractValidator<AddProductToCategoryCommand>
{
    public AddProductToCategoryCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
