using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

# pragma warning disable

public sealed class Order : AggregateRoot, IAuditableEntity
{
    public Order(
        Guid id,
        Amount totalAmount,
        User user,
        Shipping shipping,
        Payment payment) : base(id)
    {
        TotalAmount = totalAmount;
        User = user;
        Shipping = shipping;
        Payment = payment;
    }

    public Amount TotalAmount { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Shipping Shipping { get; set; }
    public Payment Payment { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
