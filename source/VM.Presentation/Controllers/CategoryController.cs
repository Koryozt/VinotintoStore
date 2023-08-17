using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Categories.Commands.AddProduct;
using VM.Application.Segregation.Categories.Commands.Create;
using VM.Application.Segregation.Categories.Commands.Update;
using VM.Application.Segregation.Categories.Queries;
using VM.Application.Segregation.Categories.Queries.Statements;
using VM.Domain.Enums;
using VM.Domain.Shared;
using VM.Infrastructure.Authentication;
using VM.Presentation.Abstractions;
using VM.Presentation.Contracts.Category;

namespace VM.Presentation.Controllers;

[Route("api/categories/")]
public sealed class CategoryController : ApiController
{
    public CategoryController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Adds a new product to a specific category.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/categories/add
    /// </remarks>
    /// <response code="204">Successful</response>
    /// <response code="400">If there's a problem adding the 
    /// product.</response>
    [HttpPost("add")]
    [HasPermission(Permission.UpdateCategory)]
    [HasPermission(Permission.UpdateProduct)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(Error))]
    public async Task<IActionResult> AddProductToCategory(
        AddProductToCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddProductToCategoryCommand(
            request.ProductId, 
            request.CategoryId);

        Result result = await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing the GUID of the 
    /// created entity.</returns>
    /// <remarks>
    /// Method: POST, endpoint: api/categories/create
    /// </remarks>
    /// <response code="201">Successful</response>
    /// <response code="400">If there's a problem creating the 
    /// category.</response>
    [HttpPost("create")]
    [HasPermission(Permission.AddCategory)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(Error))]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.Name);

        Result<Guid> result= await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(HttpContext.Request.Path, result.Value);
    }

    /// <summary>
    /// Updates the name of an existing category.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult</returns>
    /// <remarks>
    /// Method: PUT, endpoint: api/categories/{id}
    /// </remarks>
    /// <response code="204">Successful</response>
    /// <response code="400">If there's a problem updating the category.</response>
    [HttpPut("{id:guid}")]
    [HasPermission(Permission.UpdateCategory)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> UpdateCategory(
        Guid id,
        [FromBody] UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(
            id,
            request.Name);

        Result result = await Sender.Send(
            command, 
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    /// <summary>
    /// Gets the matching category with the ID provided.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a 
    /// CategoryResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/categories/{id}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem getting 
    /// the category.</response>
    [HttpGet("{id:guid}")]
    [HasPermission(Permission.ReadCategory)]
    [ProducesResponseType(StatusCodes.Status200OK, 
        Type =typeof(CategoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> GetCategoryById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(id);

        Result<CategoryResponse> result = await Sender.Send(
            query, 
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) 
            : Ok(result.Value);
    }

    /// <summary>
    /// Gets the matching category with the name provided.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a CategoryResponse object.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/categories/by?name=value
    /// </remarks>
    /// <response code="200">A not null CategoryResponse object.</response>
    /// <response code="400">If there's a problem adding the product.</response>
    [HttpGet("by")]
    [HasPermission(Permission.ReadCategory)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> GetCategoryByName(
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByNameQuery(name);

        Result<CategoryResponse> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    /// <summary>
    /// Gets the matching category with the Product ID provided.
    /// </summary>
    /// <param name="id">The product ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult containing a IEnumerable collection of 
    /// CategoryResponse objects.</returns>
    /// <remarks>
    /// Method: GET, endpoint: api/categories/p/{productId}
    /// </remarks>
    /// <response code="200">Successful.</response>
    /// <response code="400">If there's a problem in getting the category.</response>
    [HttpGet("p/{id:guid}")]
    [HasPermission(Permission.ReadCategory)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public async Task<IActionResult> GetCategoriesByProduct(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoriesByProductQuery(id);

        Result<IEnumerable<CategoryResponse>> result = await Sender.Send(
            query,
            cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }
}
