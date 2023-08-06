using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Commands.Create;
using VM.Domain.Shared;

namespace VM.Application.Segregation.CartItems.Commands;

internal sealed class CartItemCommandHandler :
    ICommandHandler<AddCartItemCommand, Guid>
{
    public Task<Result<Guid>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
