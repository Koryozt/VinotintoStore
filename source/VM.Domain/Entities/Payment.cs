using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

public sealed class Payment : AggregateRoot, IAuditableEntity
{
    public Payment(
        Guid id,
        Name method,
        Amount amount,
        Order order) : base(id)
    {
        Method = method;
        Amount = amount;
        Order = order;
    }

    public Name Method { get; set; }
    public Amount Amount { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
