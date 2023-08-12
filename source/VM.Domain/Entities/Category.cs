using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

#pragma warning disable

public sealed class Category : AggregateRoot, IAuditableEntity
{
    private readonly List<Product> _products = new();

    private Category(Guid id, Name name) : base(id)
    {
        Name = name;
    }

    public Name Name { get; set; }
    public IReadOnlyCollection<Product> Products => _products;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Category Create(Guid id, Name name) =>
        new Category(id, name)
        {
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };
    
    public void AddProduct(Product product) => _products.Add(product);

    public void ChangeName(Name name) => Name = name;
}
