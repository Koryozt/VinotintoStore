using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface IRatingRepository
{
    Task<Rating?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Rating>> GetByConditionAsync(
        Expression<Func<IEnumerable<Rating>, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(Rating rating, CancellationToken cancellationToken);
    void Update(Rating order);
}
