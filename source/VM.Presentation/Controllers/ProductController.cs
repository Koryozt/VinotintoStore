using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Products.Commands.Create;
using VM.Application.Segregation.Products.Commands.Update;
using VM.Application.Segregation.Products.Queries;
using VM.Application.Segregation.Products.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.Product;

namespace VM.Presentation.Controllers;

[Route("api/products/")]
public sealed class ProductController : ApiController
{
    public ProductController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [HasPermission(Permission.AddProduct)]
    public async Task<IActionResult> AddProduct(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Photo,
            request.Name,
            request.Description,
            request.Price,
            request.Stock);

        Result<Guid> result = await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = result.Value },
            result.Value);
    }

    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadProduct)]
    public async Task<IActionResult> GetProductById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);

        Result<ProductResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet("c")]
    [HasPermission(Permission.ReadProduct)]
    public async Task<IActionResult> GetProductByCategory(
    [FromQuery] string categoryName,
    CancellationToken cancellationToken)
    {
        var query = new GetProductsByCategoryQuery(categoryName);

        Result<IEnumerable<ProductResponse>> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet("name")]
    [HasPermission(Permission.ReadProduct)]
    public async Task<IActionResult> GetProductByName(
    string? name,
    CancellationToken cancellationToken)
    {
        var query = new GetProductByNameQuery(name);

        Result<ProductResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPut]
    [HasPermission(Permission.ReadProduct)]
    [HasPermission(Permission.UpdateProduct)]
    public async Task<IActionResult> UpdateProduct(
    [FromBody] UpdateProductRequest request,
    CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            request.Id,
            request.Name,
            request.Description,
            request.Price,
            request.Stock);

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
