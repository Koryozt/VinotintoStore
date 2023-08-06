using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.OrderDetails.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.OrderDetails.Queries;

internal sealed class OrderDetailsQueryHandler :
    IQueryHandler<GetOrderDetailByIdQuery, OrderDetailResponse>,
    IQueryHandler<GetOrderDetailsByOrderQuery, IEnumerable<OrderDetailResponse>>
{
    public Task<Result<OrderDetailResponse>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<OrderDetailResponse>>> Handle(GetOrderDetailsByOrderQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
