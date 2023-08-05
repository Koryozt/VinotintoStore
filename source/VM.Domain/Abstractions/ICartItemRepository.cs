using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;

public interface ICartItemRepository
{
    Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<CartItem>> GetByConditionAsync(
        Expression<Func<IEnumerable<CartItem>, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(CartItem cartItem, CancellationToken cancellationToken);
    void Update(CartItem cartItem);
}
