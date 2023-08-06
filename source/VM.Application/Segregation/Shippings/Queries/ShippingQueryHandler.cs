using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Shippings.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Shippings.Queries;

internal sealed class ShippingQueryHandler :
    IQueryHandler<GetShippingByIdQuery, ShippingResponse>,
    IQueryHandler<GetShippingByOrderQuery, ShippingResponse>
{
    public Task<Result<ShippingResponse>> Handle(GetShippingByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ShippingResponse>> Handle(GetShippingByOrderQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
