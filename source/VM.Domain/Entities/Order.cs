using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.DomainEvents;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

# pragma warning disable

public sealed class Order : AggregateRoot, IAuditableEntity
{
    private readonly List<OrderDetail> _orderDetails = new();

    private Order(
        Guid id,
        Amount totalAmount) : base(id)
    {
        TotalAmount = totalAmount;
        IsCanceled = true;
    }

    private Order()
    {
    }

    public Amount TotalAmount { get; set; }
    public bool IsCanceled { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ShippingId { get; set; }
    public Shipping Shipping { get; set; }
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; }
    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public void AddOrderDetail(OrderDetail orderDetail) =>
        _orderDetails.Add(orderDetail);

    public static Order Create(
        Guid id,
        Amount totalAmount,
        User user,
        Shipping shipping,
        Payment payment)
    {
        var order = new Order(
            id,
            totalAmount)
        {
            UserId = user.Id,
            User = user,
            ShippingId = shipping.Id,
            Shipping = shipping,
            PaymentId = payment.Id,
            Payment = payment,
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };

        order.RaiseDomainEvent(new OrderMadeDomainEvent(
            Guid.NewGuid(),
            order.Id));

        return order;

    }

    public void AddEstablishedRelationships(Payment payment, Shipping shipping)
    {
        Payment = payment;
        Shipping = shipping;
    }

    public void ChangeStatus(bool cancel)
    {
        if (!IsCanceled.Equals(cancel))
        {
            RaiseDomainEvent(new OrderStatusModifiedDomainEvent(
                Guid.NewGuid(),
                Id));
        }

        IsCanceled = cancel;
    }
}
