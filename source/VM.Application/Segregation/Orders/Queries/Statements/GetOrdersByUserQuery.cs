using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Orders.Queries.Statements;

public sealed record GetOrdersByUserQuery(Guid UserId) : 
    IQuery<IEnumerable<OrderResponse>>;