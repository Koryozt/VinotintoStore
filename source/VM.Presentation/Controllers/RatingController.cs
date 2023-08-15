using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Ratings.Commands.AddRating;
using VM.Application.Segregation.Ratings.Commands.Update;
using VM.Application.Segregation.Ratings.Queries;
using VM.Application.Segregation.Ratings.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.Rating;

namespace VM.Presentation.Controllers;

[Route("api/ratings/")]
public sealed class RatingController : ApiController
{
    public RatingController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadRating)]
    public async Task<IActionResult> GetRatingById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetRatingByIdQuery(id);

        Result<RatingResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet("p/{id:guid}")]
    [HasPermission(Permission.ReadRating)]
    public async Task<IActionResult> GetRatingsByProduct(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetRatingsByProductQuery(id);

        Result<IEnumerable<RatingResponse>> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost]
    [HasPermission(Permission.AddRating)]
    public async Task<IActionResult> AddRating(
        AddRatingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddRatingCommand(
            request.Score,
            request.Comment,
            request.ProductId
            );

        Result<Guid> result = await Sender.Send(
            command,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : 
            Created(HttpContext.Request.Path, result.Value);
    }

    [HttpPatch]
    [HasPermission(Permission.AddRating)]
    public async Task<IActionResult> UpdateRating(
        UpdateRatingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRatingCommand(
            request.Id,
            request.Score,
            request.Comment
            );

        Result result = await Sender.Send(
            command,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) :
           NoContent();
    }

}
