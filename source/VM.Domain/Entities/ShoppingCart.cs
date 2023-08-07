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
    private readonly List<CartItem> _cartItems = new();

    private ShoppingCart(Guid id) : base(id)
    {
    }

    public Guid UserId { get; set; }
    public User User { get; set; }
    public IReadOnlyCollection<CartItem> CartItems => _cartItems;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
