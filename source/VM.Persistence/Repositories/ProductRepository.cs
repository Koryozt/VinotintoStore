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

public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Product>()
            .AddAsync(product, cancellationToken);

    public async Task<IEnumerable<Product>> GetByConditionAsync(
        Expression<Func<Product, bool>> condition,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Product>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken) =>
        await _context
            .Set<Product>()
            .FirstOrDefaultAsync(p => p.Id == id);

    public void Update(Product product) =>
        _context.Set<Product>().Update(product);
}
