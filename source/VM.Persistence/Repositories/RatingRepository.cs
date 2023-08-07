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

public sealed class RatingRepository : IRatingRepository
{
    private readonly ApplicationDbContext _context;

    public RatingRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        Rating rating,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Rating>()
            .AddAsync(rating, cancellationToken);

    public async Task<IEnumerable<Rating>> GetByConditionAsync(
        Expression<Func<Rating, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Rating>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Rating?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Rating>()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public void Update(Rating rating) =>
        _context.Set<Rating>().Update(rating);
}
