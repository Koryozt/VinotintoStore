using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.CartItems.Commands.Create;
using VM.Application.Segregation.ShoppingCarts.Queries;
using VM.Application.Segregation.ShoppingCarts.Queries.Statements;
using VM.Application.Segregation.Users.Queries;
using VM.Application.Segregation.Users.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.ShoppingCart;

namespace VM.Presentation.Controllers;

[Route("api/carts/")]
public sealed class ShoppingCartController : ApiController
{
    public ShoppingCartController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadUser)]
    [HasPermission(Permission.ReadShoppingCart)]
    public async Task<IActionResult> GetCartById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetShoppingCartByIdQuery(id);

        Result<ShoppingCartResponse> response = await Sender.Send(
            query,
            cancellationToken);

        return response.IsFailure ? BadRequest(response.Error) :
            Ok(response.Value);
    }

    [HttpGet("u/{userId:guid}")]
    [HasPermission(Permission.ReadUser)]
    [HasPermission(Permission.ReadShoppingCart)]
    public async Task<IActionResult> GetCartByUser(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var query = new GetShoppingCartByUserQuery(userId);

        Result<ShoppingCartResponse> response = await Sender.Send(
            query,
            cancellationToken);

        return response.IsFailure ? BadRequest(response.Error) :
            Ok(response.Value);
    }

    [HttpGet("mine")]
    [HasPermission(Permission.ReadUser)]
    [HasPermission(Permission.ReadShoppingCart)]
    public async Task<IActionResult> GetLoggedUserCart(
    CancellationToken cancellationToken)
    {
        var userId = GetLoggedUserIdentifier();

        var query = new GetShoppingCartByUserQuery(userId);

        Result<ShoppingCartResponse> response = await Sender.Send(
            query,
            cancellationToken);

        return response.IsFailure ? BadRequest(response.Error) :
            Ok(response.Value);
    }

    [HttpPost]
    [HasPermission(Permission.ReadShoppingCart)]
    [HasPermission(Permission.ReadUser)]
    [HasPermission(Permission.UpdateCurrentUser)]
    public async Task<IActionResult> AddCartItem(
        AddCartItemRequest request,
        CancellationToken cancellationToken
        )
    {
        var userId = GetLoggedUserIdentifier();
        var shoppingCartQuery = new GetShoppingCartByUserQuery(userId);

        ShoppingCartResponse cartResponse = (await Sender.Send(
            shoppingCartQuery,
            cancellationToken)).Value;

        var command = new AddCartItemCommand(
            request.Quantity,
            request.TotalPrice,
            request.ProductId,
            cartResponse.Id);

        Result<Guid> response = await Sender.Send(
            command,
            cancellationToken);

        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok(response.Value);
    }
}
