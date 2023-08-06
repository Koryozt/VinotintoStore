using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Coupons.Commands.Create;
using VM.Application.Segregation.Coupons.Commands.Update;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Coupons.Commands;
internal sealed class CouponCommandHandler :
    ICommandHandler<CreateCouponCommand, Guid>,
    ICommandHandler<ChangeCouponAvailabilityCommand>
{
    public Task<Result<Guid>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(ChangeCouponAvailabilityCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
