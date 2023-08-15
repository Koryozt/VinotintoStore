using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Orders.Commands.Create;
using VM.Application.Segregation.Orders.Commands.Update;
using VM.Application.Segregation.Orders.Queries;
using VM.Application.Segregation.Orders.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.Order;

namespace VM.Presentation.Controllers;

[Route("api/order/")]
public sealed class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadOrder)]
    public async Task<IActionResult> GetOrderById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(id);

        Result<OrderResponse> result = await Sender.Send(
            query, 
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : 
            Ok(result.Value);
    }

    [HttpGet("u/{id:guid}")]
    [HasPermission(Permission.ReadOrder)]
    public async Task<IActionResult> GetOrdersByUser(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetOrdersByUserQuery(id);

        Result<IEnumerable<OrderResponse>> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) :
            Ok(result.Value);
    }

    [HttpPost]
    [HasPermission(Permission.CreateOrder)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        Guid userId = GetLoggedUserIdentifier();

        var command = new CreateOrderCommand(
            request.Method,
            request.Address,
            request.Amount,
            request.Cost,
            userId);

        Result<Guid> result = await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
             nameof(GetOrderById),
            new { id = result.Value },
            result.Value);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permission.UpdateOrder)]
    public async Task<IActionResult> UpdateOrder(
        Guid id,
        [FromBody] bool cancel,
        CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand(id, cancel);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
