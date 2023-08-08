using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.Ratings.Queries;

namespace VM.Application.Segregation.CartItems.Queries;

public sealed record CartItemProductResponse(
    Guid Id,
    string Name,
    string Description,
    double Price,
    int Stock);