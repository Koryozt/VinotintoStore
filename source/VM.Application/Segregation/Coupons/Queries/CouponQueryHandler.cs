using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Coupons.Queries.Statements;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Queries;

internal sealed class CouponQueryHandler :
    IQueryHandler<GetCouponByCodeQuery, CouponResponse>,
    IQueryHandler<GetCouponByIdQuery, CouponResponse>,
    IQueryHandler<GetAmountWithCouponQuery, Amount>
{
    public Task<Result<CouponResponse>> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CouponResponse>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Amount>> Handle(GetAmountWithCouponQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
