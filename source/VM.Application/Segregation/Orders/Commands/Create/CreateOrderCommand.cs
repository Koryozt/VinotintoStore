using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Orders.Commands.Create;

// Total amount will be cost + amount. Amount will be the summatory of each total amount in OrderDetail (Quantity * Price).

// In case I don't remember, this class will create first a new Payment entity, then a Shipping entity and finally an order entity,
// after that, I will add the values to the respectives properties in the classes.

public sealed record CreateOrderCommand(
    string Method,
    string Address,
    double Amount,
    double Cost,
    Guid UserId) : ICommand<Guid>;