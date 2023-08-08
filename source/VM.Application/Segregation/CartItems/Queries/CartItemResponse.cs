using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.CartItems.Queries.Statements;
using VM.Application.Segregation.ShoppingCarts.Queries;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.CartItems.Queries;

public sealed record CartItemResponse(
    Guid Id,
    int Quantity,
    double TotalPrice,
    CartItemProductResponse Product,
    CartItemShoppingCartResponse ShoppingCart);