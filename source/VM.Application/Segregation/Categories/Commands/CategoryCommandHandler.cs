using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Categories.Commands.AddProduct;
using VM.Application.Segregation.Categories.Commands.Create;
using VM.Application.Segregation.Categories.Commands.Update;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Categories.Commands;

internal sealed class CategoryCommandHandler :
    ICommandHandler<CreateCategoryCommand, Guid>,
    ICommandHandler<AddProductToCategoryCommand>,
    ICommandHandler<UpdateCategoryCommand>
{
    public Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
