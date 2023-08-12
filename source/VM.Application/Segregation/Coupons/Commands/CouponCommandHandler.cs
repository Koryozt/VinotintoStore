using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Coupons.Commands.Create;
using VM.Application.Segregation.Coupons.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Commands;
internal sealed class CouponCommandHandler :
    ICommandHandler<CreateCouponCommand, Guid>,
    ICommandHandler<ChangeCouponAvailabilityCommand>
{
    private readonly ICouponRepository _couponRepository;
    private readonly IUnitOfWork _uow;

    public CouponCommandHandler(
        ICouponRepository couponRepository,
        IUnitOfWork uow)
    {
        _couponRepository = couponRepository;
        _uow = uow;
    }


    public async Task<Result<Guid>> Handle(
        CreateCouponCommand request,
        CancellationToken cancellationToken)
    {
        Result<Code> codeResult = Code.Create(request.Code);
        Result<Amount> discountResult = Amount.Create(request.Discount);

        var coupon = Coupon.Create(
            Guid.NewGuid(),
            codeResult.Value,
            discountResult.Value,
            request.ExpirationDate,
            request.IsActive);

        await _couponRepository.AddAsync(coupon, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return coupon.Id;
    }

    public async Task<Result> Handle(
        ChangeCouponAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var coupon = await _couponRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (coupon is null)
        {
            return Result.Failure(DomainErrors.Coupon.NotFound(
                request.Id.ToString()));
        }

        coupon.ChangeAvailability(request.IsActive);

        _couponRepository.Update(coupon);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
