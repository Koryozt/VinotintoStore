using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetByConditionAsync(
        Expression<Func<IEnumerable<Order>, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(Order order, CancellationToken cancellationToken);
    void Update(Order order);
}
