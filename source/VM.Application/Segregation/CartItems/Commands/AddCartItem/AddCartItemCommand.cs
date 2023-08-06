using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.CartItems.Commands.Create;

public sealed record AddCartItemCommand(
    int Quantity,
    double TotalPrice,
    Guid ProductId,
    Guid ShoppingCartId) : ICommand<Guid>;