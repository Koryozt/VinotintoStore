using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.OrderDetails.Commands.AddDetail;

public sealed record AddOrderDetailCommand(
    int Quantity,
    double Price,
    Guid ProductId,
    Guid OrderId) : ICommand<Guid>;