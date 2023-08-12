using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.CartItems.Queries;
using VM.Application.Segregation.Products.Queries;
using VM.Application.Segregation.Users.Queries;

namespace VM.Application.Segregation.ShoppingCarts.Queries;

public sealed record ShoppingCartResponse(
    Guid Id,
    ShoppingCartUserResponse User,
    IEnumerable<ShoppingCartItemResponse> Items);