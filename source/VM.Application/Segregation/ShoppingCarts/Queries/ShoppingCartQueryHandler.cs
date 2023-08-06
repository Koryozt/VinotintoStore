using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.ShoppingCarts.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.ShoppingCarts.Queries;

internal sealed class ShoppingCartQueryHandler :
    IQueryHandler<GetShoppingCartByIdQuery, ShoppingCartResponse>,
    IQueryHandler<GetShoppingCartByUserQuery, ShoppingCartResponse>
{
    public Task<Result<ShoppingCartResponse>> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ShoppingCartResponse>> Handle(GetShoppingCartByUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
