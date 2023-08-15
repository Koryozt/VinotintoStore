using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

public sealed class CartItem : AggregateRoot, IAuditableEntity
{
    private CartItem(
        Guid id,
        Quantity quantity,
        Amount totalPrice) : base(id)
    {
        Quantity = quantity;
        TotalPrice = totalPrice;
    }

    public Quantity Quantity { get; set; }
    public Amount TotalPrice { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShoppingCartId { get; set; }
    public Product Product { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static CartItem AddItemToShoppingCart(
        Guid id,
        Amount totalPrice,
        Quantity quantity,
        Product cartProduct,
        ShoppingCart shoppingCart)
    {
        var cartItem = new CartItem(
            id,
            quantity,
            totalPrice)
        {
            ProductId = cartProduct.Id,
            ShoppingCartId = shoppingCart.Id,
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };

        return cartItem;
    }
}
