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

    private ShoppingCart(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
    public User User { get; set; }
    public IReadOnlyCollection<CartItem> CartItems => _cartItems;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static ShoppingCart Create(
        Guid id,
        User user)
    {
        var shoppingCart = new ShoppingCart(id, user.Id)
        {
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };

        return shoppingCart;
    }

    public void AddNewItem(CartItem cartItem) => 
        _cartItems.Add(cartItem);
}
