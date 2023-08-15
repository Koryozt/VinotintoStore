using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HasPermission(Permission.ReadUser)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        Result<UserResponse> result = await 
            Sender.Send(query, cancellationToken);

        return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
    }

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
