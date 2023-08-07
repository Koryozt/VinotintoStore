using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Persistence.Repositories;

# pragma warning disable

public sealed class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _context;

    public CouponRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        Coupon coupon, 
        CancellationToken cancellationToken) =>
        await _context
            .Set<Coupon>()
            .AddAsync(coupon, cancellationToken);

    public async Task<IEnumerable<Coupon>> GetByConditionAsync(
        Expression<Func<Coupon, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Coupon>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Coupon?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Coupon>()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public void Update(Coupon coupon) =>
        _context.Set<Coupon>().Update(coupon);
}
