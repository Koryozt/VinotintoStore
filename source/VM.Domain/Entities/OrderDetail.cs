using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

public sealed class OrderDetail : AggregateRoot, IAuditableEntity
{
    public OrderDetail(
        Guid id,
        Quantity quantity,
        Amount price,
        Product product,
        Order order) : base(id)
    {
        Quantity = quantity;
        Price = price;
        Product = product;
        Order = order;
    }

    public Quantity Quantity { get; set; }
    public Amount Price { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
