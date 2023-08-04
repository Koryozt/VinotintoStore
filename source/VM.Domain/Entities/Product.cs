using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

#pragma warning disable

public sealed class Product : AggregateRoot, IAuditableEntity
{
    public Product(
        Guid id,
        Name name,
        LongText description,
        Amount price,
        Quantity stock) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public Name Name { get; set; }
    public LongText Description { get; set; }
    public Amount Price { get; set; }
    public Quantity Stock { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
