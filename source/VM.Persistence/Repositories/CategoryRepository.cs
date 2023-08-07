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

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        Category category,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Category>()
            .AddAsync(category, cancellationToken);

    public async Task<IEnumerable<Category>> GetByConditionAsync(
        Expression<Func<Category, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Category>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Category?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Category>()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public void Update(Category category) =>
        _context.Set<Category>().Update(category);
}
