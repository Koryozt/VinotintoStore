﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

public sealed class Shipping : AggregateRoot, IAuditableEntity
{
    public Shipping(
        Guid id,
        LongText address,
        Amount cost,
        Order order) : base(id)
    {
        Address = address;
        Cost = cost;
        Order = order;
    }

    public LongText Address { get; set; }
    public Amount Cost { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
