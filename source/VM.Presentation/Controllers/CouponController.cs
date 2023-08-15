using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Coupons.Commands.Create;
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

    [HttpPost]
    [HasPermission(Permission.CreateCoupon)]
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

    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadCoupon)]
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

    [HttpGet("c")]
    [HasPermission(Permission.ReadCoupon)]
    public async Task<IActionResult> GetCouponByCode(
    [FromQuery] string code,
    CancellationToken cancellationToken)
    {
        var query = new GetCouponByCodeQuery(code);

        Result<CouponResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) :
            Ok(result.Value);
    }
}
