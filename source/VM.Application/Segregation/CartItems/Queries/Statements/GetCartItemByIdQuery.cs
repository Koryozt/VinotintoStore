using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.CartItems.Queries.Statements;

public sealed record GetCartItemByIdQuery(Guid id) : IQuery<CartItemResponse>;