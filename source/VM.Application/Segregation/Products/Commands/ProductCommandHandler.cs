using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Products.Commands.Create;
using VM.Application.Segregation.Products.Commands.Update;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Products.Commands;

internal sealed class ProductCommandHandler :
    ICommandHandler<CreateProductCommand, Guid>,
    ICommandHandler<UpdateProductCommand>
{
    public Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
