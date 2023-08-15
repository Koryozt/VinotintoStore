using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.OrderDetails.Queries;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Orders.Queries;

public sealed record OrderDetailsResponse(
    Guid Id,
    int Quantity,
    double Price,
    OrderDetailProductResponse Product);