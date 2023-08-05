using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;

namespace VM.Domain.Entities;

# pragma warning disable

public sealed class ShoppingCart : AggregateRoot, IAuditableEntity
{
    private ShoppingCart(
        Guid id,
        User user) : base(id)
    {
        User = user;
    }

    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
