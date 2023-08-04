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
    public Category(Guid id, Name name) : base(id)
    {
        Name = name;
    }

    public Name Name { get; set; }
    public ICollection<Product> Products { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
