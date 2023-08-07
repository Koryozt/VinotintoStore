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

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Payment>()
            .AddAsync(payment, cancellationToken);

    public async Task<IEnumerable<Payment>> GetByConditionAsync(
        Expression<Func<Payment, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Payment>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Payment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Payment>()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public void Update(Payment payment) =>
        _context.Set<Payment>().Update(payment);
}
