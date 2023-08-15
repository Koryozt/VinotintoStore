using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.ShoppingCart;

public sealed record AddCartItemRequest(
    int Quantity,
    double TotalPrice,
    Guid ProductId);