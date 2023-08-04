using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

public sealed class Coupon : AggregateRoot, IAuditableEntity
{
    private Coupon(
        Guid id,
        Code code,
        Amount discount,
        DateTime expirationDate,
        bool isActive) : base(id)
    {
        Code = code;
        Discount = discount;
        ExpirationDate = expirationDate;
        IsActive = isActive;
    }

    public Code Code { get; set; }
    public Amount Discount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
