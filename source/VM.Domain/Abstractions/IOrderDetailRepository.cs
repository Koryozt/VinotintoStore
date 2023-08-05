using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface IOrderDetailRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderDetail>> GetByConditionAsync(
        Expression<Func<IEnumerable<OrderDetail>, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(OrderDetail orderDetail, CancellationToken cancellationToken);
    void Update(OrderDetail orderDetail);
}
