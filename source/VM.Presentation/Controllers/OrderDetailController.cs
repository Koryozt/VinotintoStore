using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
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
