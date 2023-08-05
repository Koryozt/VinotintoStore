using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface IShopingCartRepository
{
    Task<ShoppingCart?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ShoppingCart>> GetByConditionAsync(
        Expression<Func<IEnumerable<ShoppingCart>, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(ShoppingCart cart, CancellationToken cancellationToken);
    void Update(ShoppingCart cart);
}
