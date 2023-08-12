using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.CartItems.Queries;

namespace VM.Application.Segregation.ShoppingCarts.Queries;

public sealed record ShoppingCartItemResponse(
    Guid Id,
    int Quantity,
    double TotalPrice,
    CartItemProductResponse Product);
