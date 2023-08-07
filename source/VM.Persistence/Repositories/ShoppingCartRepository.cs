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

public sealed class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ApplicationDbContext _context;

    public ShoppingCartRepository(ApplicationDbContext context) => 
        _context = context;

    public async Task AddAsync(
        ShoppingCart cart,
        CancellationToken cancellationToken) =>
        await _context
            .Set<ShoppingCart>()
            .AddAsync(cart, cancellationToken);

    public async Task<IEnumerable<ShoppingCart>> GetByConditionAsync(
        Expression<Func<ShoppingCart, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<ShoppingCart>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<ShoppingCart?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<ShoppingCart>()
            .FirstOrDefaultAsync(sc => sc.Id == id, cancellationToken);

    public void Update(ShoppingCart cart) =>
        _context.Set<ShoppingCart>().Update(cart);
}
