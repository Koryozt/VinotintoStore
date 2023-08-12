using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.OrderDetails.Queries;

public sealed record OrderDetailResponse(
    Guid Id,
    Quantity Quantity,
    Amount Price,
    OrderDetailProductResponse Product,
    OrderDetailOrderResponse Order);