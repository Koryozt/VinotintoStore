using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.CartItems.Queries;

public sealed record CartItemResponse(
    Guid Id,
    Quantity Quantity,
    Amount TotalPrice,
    Product Product,
    ShoppingCart ShoppingCart);