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

public sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        Order order, 
        CancellationToken cancellationToken) =>
        await _context
            .Set<Order>()
            .AddAsync(order, cancellationToken);

    public async Task<IEnumerable<Order>> GetByConditionAsync(
        Expression<Func<Order, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Order>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Order>()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public void Update(Order order) =>
        _context.Set<Order>().Update(order);
}
