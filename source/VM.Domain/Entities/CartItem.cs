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
    public CartItem(
        Guid id,
        Quantity quantity,
        Amount totalPrice,
        Product product,
        ShoppingCart shoppingCart) : base(id)
    {
        Quantity = quantity;
        TotalPrice = totalPrice;
        Product = product;
        ShoppingCart = shoppingCart;
    }

    public Quantity Quantity { get; set; }
    public Amount TotalPrice { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShoppingCartId { get; set; }
    public Product Product { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
