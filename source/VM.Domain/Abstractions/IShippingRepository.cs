using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface IShippingRepository
{
    Task<Shipping?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Shipping>> GetByConditionAsync(
        Expression<Func<Shipping, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(Shipping shipping, CancellationToken cancellationToken);
    void Update(Shipping shipping);
}
