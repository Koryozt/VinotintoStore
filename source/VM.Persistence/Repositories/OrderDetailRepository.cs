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

public sealed class OrderDetailRepository : IOrderDetailRepository
{
    private readonly ApplicationDbContext _context;

    public OrderDetailRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        OrderDetail orderDetail,
        CancellationToken cancellationToken) =>
        await _context
            .Set<OrderDetail>()
            .AddAsync(orderDetail, cancellationToken);

    public async Task<IEnumerable<OrderDetail>> GetByConditionAsync(
        Expression<Func<OrderDetail, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<OrderDetail>()
            .Where(condition)
            .ToListAsync();

    public async Task<OrderDetail?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<OrderDetail>()
            .FirstOrDefaultAsync(od => od.Id == id, cancellationToken);

    public void Update(OrderDetail orderDetail) =>
        _context.Set<OrderDetail>().Update(orderDetail);

}
