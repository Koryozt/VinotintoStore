using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Photo,
    string Name,
    string Description,
    double Price,
    int Stock) : ICommand<Guid>;
