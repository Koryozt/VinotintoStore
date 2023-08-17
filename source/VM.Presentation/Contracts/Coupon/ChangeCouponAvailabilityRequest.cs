using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.Coupon;

public sealed record ChangeCouponAvailabilityRequest(
    bool IsActive);