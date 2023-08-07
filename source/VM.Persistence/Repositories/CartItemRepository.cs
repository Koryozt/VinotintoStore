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

public sealed class CartItemRepository : ICartItemRepository
{
    private readonly ApplicationDbContext _context;

    public CartItemRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(CartItem cartItem, 
        CancellationToken cancellationToken) =>
        await _context.Set<CartItem>().AddAsync(cartItem, cancellationToken);

    public async Task<IEnumerable<CartItem>> GetByConditionAsync(
        Expression<Func<CartItem, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<CartItem>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<CartItem?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken) =>
        await _context
            .Set<CartItem>()
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

    public void Update(CartItem cartItem) =>
        _context.Set<CartItem>().Update(cartItem);
}
