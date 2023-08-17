using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
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

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the ID of the created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/products/
    /// </remarks>
    /// <response code="201">Successful.</response>
    /// <response code="400">If there's a problem adding the product.</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null ProductResponse object.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/products/
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the product.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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


    /// <summary>
    /// Gets the products that matches with the provided category name.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null Collection of ProductResponse objects.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/products/c?categoryName=value
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the products.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
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

    /// <summary>
    /// Gets the products that matches with the provided name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a not null ProductResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/products/n?name=value
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting the product.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpGet("n")]
    [HasPermission(Permission.ReadProduct)]
    public async Task<IActionResult> GetProductByName(
    string name,
    CancellationToken cancellationToken)
    {
        var query = new GetProductByNameQuery(name);

        Result<ProductResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    /// <summary>
    /// Updates the products that matches with the provided id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult</returns>
    /// <remarks>
    /// Method: PUT, endpoint: api/products/{id}
    /// </remarks>
    /// <response code="204">Successful.</response>
    /// <response code="400">If there's a problem getting the products.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [HttpPut("{id:guid}")]
    [HasPermission(Permission.ReadProduct)]
    [HasPermission(Permission.UpdateProduct)]
    public async Task<IActionResult> UpdateProduct(
        Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            id,
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
