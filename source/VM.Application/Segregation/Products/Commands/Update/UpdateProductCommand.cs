using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Products.Commands.Update;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    double Price,
    int Stock) : ICommand;
