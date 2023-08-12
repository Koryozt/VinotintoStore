using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.OrderDetails.Queries;

public sealed record OrderDetailProductResponse(
    Guid Id,
    Name Name,
    Amount Price,
    Quantity Stock);
