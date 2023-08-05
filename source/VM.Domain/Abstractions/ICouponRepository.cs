using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Abstractions;

public interface ICouponRepository
{
    Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Coupon>> GetByConditionAsync(
        Expression<Func<IEnumerable<Coupon>, bool>> condition, CancellationToken cancellationToken);
    Task<Amount> GetProductAmountWithDiscount(
        Code code, Amount amount, CancellationToken cancellationToken);
    Task AddAsync(Coupon coupon, CancellationToken cancellationToken);
    void Update(Coupon coupon);
}
