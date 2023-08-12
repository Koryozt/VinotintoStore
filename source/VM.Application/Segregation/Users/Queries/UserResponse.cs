using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.ShoppingCarts.Queries;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Users.Queries;

public sealed record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    UserShoppingCartResponse ShoppingCart);