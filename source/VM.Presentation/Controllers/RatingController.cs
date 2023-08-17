using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Ratings.Commands.AddRating;
using VM.Application.Segregation.Ratings.Commands.Update;
using VM.Application.Segregation.Ratings.Queries;
using VM.Application.Segregation.Ratings.Queries.Statements;
using VM.Application.Segregation.ShoppingCarts.Queries;
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

    /// <summary>
    /// Gets the Rating that matches with the ID provided.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null RatingResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/ratings/{id}
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="400">If there's a problem getting the rating.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RatingResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Gets the Ratings that matches with the ProductID provided.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null collection of RatingResponse objects.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/ratings/p/{id}
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="400">If there's a problem getting the rating.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RatingResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Creates a new rating for a specific product.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the GUID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/ratings/
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="400">If there's a problem creating the rating.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Updates the rating that matches with the ID provided.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult.</returns>
    /// <remarks>
    /// Method: PATCH, endpoint: api/ratings/
    /// </remarks>
    /// <response code="204">Successful.</response>
    /// <response code="400">If there's a problem getting the rating.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPatch("{id:guid}")]
    [HasPermission(Permission.AddRating)]
    public async Task<IActionResult> UpdateRating(
        Guid id,
        UpdateRatingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRatingCommand(
            id,
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
