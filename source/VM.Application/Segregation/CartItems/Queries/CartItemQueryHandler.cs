using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.CartItems.Queries;

internal sealed class CartItemQueryHandler :
    IQueryHandler<GetCartItemByIdQuery, CartItemResponse>,
    IQueryHandler<GetCartItemByProductQuery, CartItemResponse>,
    IQueryHandler<GetCartItemsByShopingCartQuery, IEnumerable<CartItemResponse>>
{
    public Task<Result<CartItemResponse>> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CartItemResponse>> Handle(GetCartItemByProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<CartItemResponse>>> Handle(GetCartItemsByShopingCartQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
