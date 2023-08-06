using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Orders.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Orders.Queries;

internal sealed class OrderQueryHandler :
    IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    public Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
