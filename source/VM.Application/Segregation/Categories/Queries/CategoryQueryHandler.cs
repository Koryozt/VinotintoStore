using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Categories.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Categories.Queries;

internal sealed class CategoryQueryHandler :
    IQueryHandler<GetCategoryByIdQuery, CategoryResponse>,
    IQueryHandler<GetCategoryByNameQuery, CategoryResponse>,
    IQueryHandler<GetCategoriesByProductQuery, IEnumerable<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoryQueryHandler(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<CategoryResponse>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (category is null)
        {
            return Result.Failure<CategoryResponse>(
                DomainErrors.Category.NotFound(request.Id.ToString()));
        }

        IReadOnlyCollection<ProductCategoryResponse> productResponse =
            category.Products
                .Select(prod => 
                    new ProductCategoryResponse(
                        prod.Id,
                        prod.Name.Value,
                        prod.Description.Value,
                        prod.Price.Value,
                        prod.Stock.Value))
                .ToList()
                .AsReadOnly();

        CategoryResponse response = new(
            category.Id,
            category.Name.Value,
            productResponse);

        return response;
    }

    public async Task<Result<CategoryResponse>> Handle(
        GetCategoryByNameQuery request,
        CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);

        var category = (await _categoryRepository.GetByConditionAsync(
            cat => nameResult.Value == cat.Name,
            cancellationToken)).FirstOrDefault();

        if (category is null)
        {
            return Result.Failure<CategoryResponse>(
                DomainErrors.Category.NotFound(request.Name));
        }

        IReadOnlyCollection<ProductCategoryResponse> productResponse =
            category.Products
                .Select(prod =>
                    new ProductCategoryResponse(
                        prod.Id,
                        prod.Name.Value,
                        prod.Description.Value,
                        prod.Price.Value,
                        prod.Stock.Value))
                .ToList()
                .AsReadOnly();

        CategoryResponse response = new(
            category.Id,
            category.Name.Value,
            productResponse);

        return response;
    }

    public async Task<Result<IEnumerable<CategoryResponse>>> Handle(
        GetCategoriesByProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(
            request.ProductId, 
            cancellationToken);

        if (product is null)
        {
            return Result.Failure<IEnumerable<CategoryResponse>>(
                DomainErrors.Category.ProductNotFound(request.ProductId));
        }

        IEnumerable<CategoryResponse> response = product.Categories
            .Select(
                cat => new CategoryResponse(
                cat.Id,
                cat.Name.Value,
                cat.Products
                .Select(prod =>
                    new ProductCategoryResponse(
                        prod.Id,
                        prod.Name.Value,
                        prod.Description.Value,
                        prod.Price.Value,
                        prod.Stock.Value))
                .ToList()
                .AsReadOnly()));

        return Result.Success(response);
    }
}
