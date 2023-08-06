using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetByConditionAsync(
        Expression<Func<Category, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(Category category, CancellationToken cancellationToken);
    void Update(Category category);
}
