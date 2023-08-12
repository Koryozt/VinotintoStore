using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Orders.Queries;

public sealed record OrderResponse(
    Guid Id,
    double TotalAmount,
    OrderUserResponse User,
    OrderShippingResponse Shipping,
    OrderPaymentResponse Payment);