using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Payments.Queries;

public sealed record PaymentOrderResponse(
    Guid Id,
    double TotalAmount);