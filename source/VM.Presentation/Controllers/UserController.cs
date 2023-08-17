using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Users.Commands.Create;
using VM.Application.Segregation.Users.Commands.Login;
using VM.Application.Segregation.Users.Commands.Update;
using VM.Application.Segregation.Users.Queries;
using VM.Application.Segregation.Users.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.User;

namespace VM.Presentation.Controllers;

[Route("api/users/")]
public sealed class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Gets the current logged user information.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null UserResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/users/me
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="404">If there's a problem getting the user.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HasPermission(Permission.ReadUser)]
    [HttpGet("me")]
    public async Task<IActionResult> MyProfile(
    CancellationToken cancellationToken)
    {
        var userId = GetLoggedUserIdentifier();

        var query = new GetUserByIdQuery(userId);

        Result<UserResponse> result = await
            Sender.Send(query, cancellationToken);

        return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
    }

    /// <summary>
    /// Gets the user that matches the ID provided.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null UserResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/users/{id}
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="404">If there's a problem getting the user.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadUser)]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        Result<UserResponse> result = await 
            Sender.Send(query, cancellationToken);

        return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
    }

    /// <summary>
    /// User login endpoint.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the JWT Token value.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/users/login
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="404">If there's a problem authenticating the user.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password);

        Result<string> result = await 
            Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// User register endpoint.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the GUID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/users/register
    /// </remarks>
    /// <response code="201">Successful</response>
    /// <response code="404">If there's a problem creating or registering the user.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            request.Firstname,
            request.Lastname,
            request.Email,
            request.Password);

        Result<Guid> result = await 
            Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = result.Value },
            result.Value);
    }

    /// <summary>
    /// Updates the current logged in user's firstname or lastname.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult.</returns>
    /// <remarks>
    /// Method: PATCH, endpoint: api/users/
    /// </remarks>
    /// <response code="204">Successful</response>
    /// <response code="400">If there's a problem updating the user.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPatch]
    [HasPermission(Permission.ReadUser)]
    [HasPermission(Permission.UpdateCurrentUser)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetLoggedUserIdentifier();

        var command = new UpdateUserCommand(
            userId,
            request.Firstname,
            request.Lastname
            );

        Result result = await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
