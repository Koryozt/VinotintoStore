using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.Users;

namespace VM.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<User?>> GetByConditionAsync(
        Expression<Func<User?, bool>> condition, CancellationToken cancellationToken);
    Task<bool> IsEmailInUseAsync(Email email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    void Update(User user);
}
