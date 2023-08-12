using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Coupons.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Queries;

internal sealed class CouponQueryHandler :
    IQueryHandler<GetCouponByCodeQuery, CouponResponse>,
    IQueryHandler<GetCouponByIdQuery, CouponResponse>,
    IQueryHandler<GetAmountWithCouponQuery, Amount>
{
    private readonly ICouponRepository _couponRepository;
    private readonly IProductRepository _productRepository;

    public CouponQueryHandler(
        ICouponRepository couponRepository,
        IProductRepository productRepository)
    {
        _couponRepository = couponRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<CouponResponse>> Handle(
        GetCouponByCodeQuery request,
        CancellationToken cancellationToken)
    {
        Result<Code> codeResult = Code.Create(request.Code);

        var coupon = (await _couponRepository.GetByConditionAsync(
            c => codeResult.Value == c.Code,
            cancellationToken)).FirstOrDefault();

        if (coupon is null)
        {
            return Result.Failure<CouponResponse>(DomainErrors.Coupon.NotFound(
                request.Code));
        }

        CouponResponse response = new(
            coupon.Id,
            coupon.Code,
            coupon.Discount,
            coupon.ExpirationDate,
            coupon.IsActive);

        return response;
    }

    public async Task<Result<CouponResponse>> Handle(
        GetCouponByIdQuery request,
        CancellationToken cancellationToken)
    {
        var coupon = await _couponRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (coupon is null)
        {
            return Result.Failure<CouponResponse>(DomainErrors.Coupon.NotFound(
                request.Id.ToString()));
        }

        CouponResponse response = new(
            coupon.Id,
            coupon.Code,
            coupon.Discount,
            coupon.ExpirationDate,
            coupon.IsActive);

        return response;
    }

    public async Task<Result<Amount>> Handle(
        GetAmountWithCouponQuery request,
        CancellationToken cancellationToken)
    {
        Result<Code> codeResult = Code.Create(request.Code);

        var coupon = (await _couponRepository.GetByConditionAsync(
            c => codeResult.Value == c.Code,
            cancellationToken)).FirstOrDefault();

        var product = await _productRepository.GetByIdAsync(
            request.ProductId, 
            cancellationToken);

        if (coupon is null)
        {
            return Result.Failure<Amount>(DomainErrors.Coupon.NotFound(
                request.Code));
        }

        if (product is null)
        {
            return Result.Failure<Amount>(DomainErrors.Coupon.ProductNotFound(
                request.ProductId));
        }

        double discount = product.Price.Value - 
            (product.Price.Value * (coupon.Discount.Value / 100));

        Result<Amount> discountResult = Amount.Create(discount);

        if (discountResult.IsFailure)
        {
            return Result.Failure<Amount>(DomainErrors.Coupon.InvalidDiscount(
                discount));
        }

        return discountResult.Value;
    }
}
