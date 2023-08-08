using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.Users.Queries;

namespace VM.Application.Segregation.CartItems.Queries.Statements;

public sealed record CartItemShoppingCartResponse(Guid ShoppingCartId);