using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.Order;

public sealed record CreateOrderRequest(
    string Method,
    string Address,
    double Amount,
    double Cost);
