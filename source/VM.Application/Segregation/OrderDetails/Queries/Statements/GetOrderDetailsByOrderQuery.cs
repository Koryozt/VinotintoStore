using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.OrderDetails.Queries.Statements;

public sealed record GetOrderDetailsByOrderQuery(Guid OrderId) : IQuery<IEnumerable<OrderDetailResponse>>;