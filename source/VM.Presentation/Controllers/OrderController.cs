using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Coupons.Queries;
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

[Route("api/orders/")]
public sealed class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Gets the matching order with the provided id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null OrderResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/orders/{id}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the order.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Gets the matching order with the provided id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null OrderResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/orders/u/{id}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the order.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Creates a new order with the logged user.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the GUID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/orders/
    /// </remarks>
    /// <response code="201">Successful.</response>
    /// <response code="400">If there's a problem creating the order.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Cancels the order that matches with the provided ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult.</returns>
    /// <remarks>
    /// Method: PUT, endpoint: api/orders/{id}
    /// </remarks>
    /// <response code="201">Successful.</response>
    /// <response code="400">If there's a problem updating the order.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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
