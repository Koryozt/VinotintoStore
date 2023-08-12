using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Application.Segregation.ShoppingCarts.Queries;

public sealed record ShoppingCartUserResponse(
    Guid Id,
    string FirstName,
    string LastName);