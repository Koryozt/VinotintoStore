using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.Users;

namespace VM.Persistence.Repositories;

# pragma warning disable

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) =>
        _context = context;

    public async Task AddAsync(User user, CancellationToken cancellationToken) =>
        await _context.Set<User>().AddAsync(user, cancellationToken);

    public async Task<IEnumerable<User?>> GetByConditionAsync(
        Expression<Func<User?, bool>> condition, 
        CancellationToken cancellationToken) =>
        await _context
            .Set<User>()
            .Where(condition)
            .ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken) =>
        await _context
            .Set<User>()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<bool> IsEmailInUseAsync(
        Email email, 
        CancellationToken cancellationToken) =>
        await _context
            .Set<User>()
            .AnyAsync(u => u.Email == email, cancellationToken);

    public void Update(User user) =>
        _context.Set<User>().Update(user);
}
