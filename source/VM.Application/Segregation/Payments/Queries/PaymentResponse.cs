using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Payments.Queries;

public sealed record PaymentResponse(
    Guid Id,
    string Method,
    double Amount,
    PaymentOrderResponse Order);