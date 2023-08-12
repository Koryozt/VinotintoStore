using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Orders.Commands.Update;

public sealed record UpdateOrderCommand(Guid Id, bool Cancel) : ICommand;