using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.OrderDetail;

public sealed record AddOrderDetailRequest(
    int Quantity,
    double Price,
    Guid ProductId,
    Guid OrderId);