using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Shippings.Queries;

public sealed record ShippingResponse(
    Guid Id,
    string Address,
    double Cost,
    Guid OrderId);