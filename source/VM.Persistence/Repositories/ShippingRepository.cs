using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM.Domain.Abstractions;
using VM.Domain.Entities;

namespace VM.Persistence.Repositories;

# pragma warning disable

public sealed class ShippingRepository : IShippingRepository
{
    private readonly ApplicationDbContext _context;

    public ShippingRepository(ApplicationDbContext context) => 
        _context = context;

    public async Task AddAsync(Shipping shipping, CancellationToken cancellationToken) =>
        await _context
            .Set<Shipping>()
            .AddAsync(shipping, cancellationToken);

    public async Task<IEnumerable<Shipping>> GetByConditionAsync(
        Expression<Func<Shipping, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Shipping>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Shipping?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Shipping>()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public void Update(Shipping shipping) =>
        _context.Set<Shipping>().Update(shipping);
}
