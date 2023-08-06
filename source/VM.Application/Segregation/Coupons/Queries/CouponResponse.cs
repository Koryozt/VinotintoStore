using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Queries;

public sealed record CouponResponse(
    Guid Id,
    Code Code,
    Amount Discount,
    DateTime ExpirationDate,
    bool IsActive);