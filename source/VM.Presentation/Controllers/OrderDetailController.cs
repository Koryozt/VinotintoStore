using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.OrderDetails.Commands.AddDetail;
using VM.Application.Segregation.OrderDetails.Queries;
using VM.Application.Segregation.OrderDetails.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.OrderDetail;

namespace VM.Presentation.Controllers;

[Route("api/order/details/")]
public sealed class OrderDetailController : ApiController
{
    public OrderDetailController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Gets the order detail that matches with the provided ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null OrderDetailResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/order/details/{id}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the order detail.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDetailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadOrderDetails)]
    public async Task<IActionResult> GetOrderDetailById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetOrderDetailByIdQuery(id);

        Result<OrderDetailResponse> result = await Sender.Send(query);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    /// <summary>
    /// Gets the order details that matches with the provided OrderID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null collection of OrderDetailResponse objects.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/order/details/o/{id}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the order detail.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDetailResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpGet("o/{id:guid}")]
    [HasPermission(Permission.ReadOrder)]
    [HasPermission(Permission.ReadOrderDetails)]
    public async Task<IActionResult> GetOrderDetailByOrder(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetOrderDetailsByOrderQuery(id);

        Result<IEnumerable<OrderDetailResponse>> result =
            await Sender.Send(query, cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    /// <summary>
    /// Gets the order details that matches with the provided OrderID.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the ID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/order/details/
    /// </remarks>
    /// <response code="201">Successful.</response>
    /// <response code="400">If there's a problem getting the order detail.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPost]
    [HasPermission(Permission.UpdateOrder)]
    public async Task<IActionResult> AddOrderDetail(
        AddOrderDetailRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddOrderDetailCommand(
            request.Quantity,
            request.Price,
            request.ProductId,
            request.OrderId);

        Result<Guid> result = await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(HttpContext.Request.Path, result.Value);
    }
}
