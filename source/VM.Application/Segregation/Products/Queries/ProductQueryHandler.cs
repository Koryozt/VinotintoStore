using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Products.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Products.Queries;

internal sealed class ProductQueryHandler :
    IQueryHandler<GetProductByIdQuery, ProductResponse>,
    IQueryHandler<GetProductByNameQuery, ProductResponse>,
    IQueryHandler<GetProductsByCategoryQuery,
        IEnumerable<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductQueryHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<ProductResponse>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(
                DomainErrors.Product.NotFound(request.Id.ToString()));
        }

        ProductResponse response = new(
            product.Id,
            product.Name.Value,
            product.Description.Value,
            product.Price.Value,
            product.Stock.Value,

            product.Categories
            .Select(
                category => new ProductCategoryResponse(
                    category.Id,
                    category.Name.Value))
            .ToList()
            .AsReadOnly(),

            product.Ratings
            .Select(
                rating => new ProductRatingResponse(
                    rating.Id,
                    rating.Score,
                    rating.Comment.Value))
            .ToList()
            .AsReadOnly());

        return response;
    }

    public async Task<Result<ProductResponse>> Handle(
        GetProductByNameQuery request,
        CancellationToken cancellationToken)
    {
        Product? product = (await _productRepository
            .GetByConditionAsync(
                prod => request.Name == prod.Name.Value,
                cancellationToken)).FirstOrDefault();

        if (product is null)
        {
            return Result.Failure<ProductResponse>(
                DomainErrors.Product.NotFound(request.Name));
        }

        ProductResponse response = new(
            product.Id,
            product.Name.Value,
            product.Description.Value,
            product.Price.Value,
            product.Stock.Value,

            product.Categories
            .Select(
                category => new ProductCategoryResponse(
                    category.Id,
                    category.Name.Value))
            .ToList()
            .AsReadOnly(),

            product.Ratings
            .Select(
                rating => new ProductRatingResponse(
                    rating.Id,
                    rating.Score,
                    rating.Comment.Value))
            .ToList()
            .AsReadOnly());

        return response;
    }

    public async Task<Result<IEnumerable<ProductResponse>>> Handle(
        GetProductsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        Category? category = (await _categoryRepository
            .GetByConditionAsync(
                cat => request.CategoryName == cat.Name.Value,
                cancellationToken)).FirstOrDefault();

        if (category is null)
        {
            return Result.Failure<IEnumerable<ProductResponse>>(
                DomainErrors.Product.CategoryNotFound(
                    request.CategoryName));
        }

        IEnumerable<ProductResponse> responses = category.Products
            .Select(product => new ProductResponse(
                product.Id,
                product.Name.Value,
                product.Description.Value,
                product.Price.Value,
                product.Stock.Value,

                product.Categories
                .Select(
                    category => new ProductCategoryResponse(
                        category.Id,
                        category.Name.Value))
                .ToList()
                .AsReadOnly(),

                product.Ratings
                .Select(
                    rating => new ProductRatingResponse(
                        rating.Id,
                        rating.Score,
                        rating.Comment.Value))
                .ToList()
                .AsReadOnly()));

        return Result.Success(responses);
    }
}
