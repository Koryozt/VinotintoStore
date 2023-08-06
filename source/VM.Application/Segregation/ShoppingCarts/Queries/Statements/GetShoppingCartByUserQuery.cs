using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.ShoppingCarts.Queries.Statements;

public sealed record class GetShoppingCartByUserQuery(Guid UserId) : IQuery<ShoppingCartResponse>;