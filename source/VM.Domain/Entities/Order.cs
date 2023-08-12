﻿using System;
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
    private readonly List<OrderDetail> _orderDetails = new();

    private Order(
        Guid id,
        Amount totalAmount) : base(id)
    {
        TotalAmount = totalAmount;
        IsCanceled = false;
    }

    private Order()
    {
    }

    public Amount TotalAmount { get; set; }
    public bool IsCanceled { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Shipping Shipping { get; set; }
    public Payment Payment { get; set; }
    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public void AddOrderDetail(OrderDetail orderDetail)
    {
        throw new NotImplementedException();
    }

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
            Shipping = shipping,
            Payment = payment,
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };

        return order;

    }

    public void AddEstablishedRelationships(Payment payment, Shipping shipping)
    {
        Payment = payment;
        Shipping = shipping;
    }

    public void ChangeStatus(bool cancel) =>
        IsCanceled = cancel;
}
