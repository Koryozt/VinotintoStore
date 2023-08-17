using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
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

    /// <summary>
    /// Gets the ShoppingCart that matches with the ID provided.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null ShoppingCartResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/carts/{id}
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="400">If there's a problem getting the cart.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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


    /// <summary>
    /// Gets the ShoppingCart that matches with the UserID provided.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null ShoppingCartResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/carts/u/{id}
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="400">If there's a problem getting the cart.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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


    /// <summary>
    /// Gets the logged in user's ShoppingCart.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null ShoppingCartResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/carts/mine/
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="400">If there's a problem getting the cart.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Adds a new product to the Shopping Cart as a CartItem.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the GUID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/carts/
    /// </remarks>
    /// <response code="201">Successful</response>
    /// <response code="400">If there's a problem adding the cart item.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPost("add")]
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
