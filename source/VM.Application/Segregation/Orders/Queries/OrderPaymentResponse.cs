using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Orders.Queries;

public sealed record OrderPaymentResponse(
    Guid Id,
    string Method,
    double Amount);