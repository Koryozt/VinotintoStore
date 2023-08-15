using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VM.Application.Segregation.Categories.Commands.AddProduct;
using VM.Application.Segregation.Categories.Commands.Create;
using VM.Application.Segregation.Categories.Commands.Update;
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

    [HttpPost("add")]
    [HasPermission(Permission.UpdateCategory)]
    [HasPermission(Permission.UpdateProduct)]
    public async Task<IActionResult> AddProductToCategory(
        AddProductToCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddProductToCategoryCommand(
            request.ProductId, 
            request.CategoryId);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpPost("create")]
    [HasPermission(Permission.AddCategory)]
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

    [HttpPut("{id:guid}")]
    [HasPermission(Permission.UpdateCategory)]
    public async Task<IActionResult> UpdateCategory(
        Guid id,
        [FromBody] UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(
            id,
            request.Name);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

}
