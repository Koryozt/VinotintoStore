using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Coupons.Commands.Create;
using VM.Application.Segregation.Coupons.Commands.Update;
using VM.Application.Segregation.Coupons.Queries;
using VM.Application.Segregation.Coupons.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.Coupon;

namespace VM.Presentation.Controllers;

[Route("api/coupons/")]
public sealed class CouponController : ApiController
{
    public CouponController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Creates a new coupon entity.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the GUID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/coupons/
    /// </remarks>
    /// <response code="201">Succesful.</response>
    /// <response code="400">If there's a problem creating the coupon.</response>
    [HttpPost]
    [HasPermission(Permission.CreateCoupon)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCoupon(
        CreateCouponRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCouponCommand(
            request.Code,
            request.Discount,
            request.ExpirationDate,
            request.IsActive);

        Result<Guid> result = await Sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(HttpContext.Request.Path, result.Value);
    }

    /// <summary>
    /// Gets the matching coupon with the provided ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null CouponResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/coupons/{id}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the coupon.</response>
    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadCoupon)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CouponResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> GetCouponById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCouponByIdQuery(id);

        Result<CouponResponse> result = await Sender.Send(
            query, 
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : 
            Ok(result.Value);
    }

    /// <summary>
    /// Gets the matching coupon with the provided code.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null CouponResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/coupons/code?value=code
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the coupon.</response>
    [HttpGet("code")]
    [HasPermission(Permission.ReadCoupon)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CouponResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> GetCouponByCode(
    [FromQuery] string value,
    CancellationToken cancellationToken)
    {
        var query = new GetCouponByCodeQuery(value);

        Result<CouponResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) :
            Ok(result.Value);
    }

    /// <summary>
    /// Updates the coupon availability.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult.</returns>
    /// <remarks>
    /// Method: PATCH, endpoint: api/coupons/{id}
    /// </remarks>
    /// <response code="204">Successful.</response>
    /// <response code="400">If there's a problem updating the coupon.</response>
    [HttpPatch("{id:guid}")]
    [HasPermission(Permission.ReadCoupon)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CouponResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> UpdateCoupon(
        Guid id,
        [FromBody] ChangeCouponAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeCouponAvailabilityCommand(
            id, 
            request.IsActive);

        Result result = await Sender.Send(
            command, 
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : 
            NoContent();
    }
}
