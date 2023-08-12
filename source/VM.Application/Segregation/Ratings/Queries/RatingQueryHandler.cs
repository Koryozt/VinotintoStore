using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Ratings.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Ratings.Queries;

internal sealed class RatingQueryHandler :
    IQueryHandler<GetRatingByIdQuery, RatingResponse>,
    IQueryHandler<GetRatingsByProductQuery, IEnumerable<RatingResponse>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IProductRepository _productRepository;

    public RatingQueryHandler(
        IRatingRepository ratingRepository,
        IProductRepository productRepository)
    {
        _ratingRepository = ratingRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<RatingResponse>> Handle(
        GetRatingByIdQuery request,
        CancellationToken cancellationToken)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (rating == null)
        {
            return Result.Failure<RatingResponse>(
                DomainErrors.Rating.NotFound(request.Id));
        }

        var response = new RatingResponse(
            rating.Id,
            rating.Score,
            rating.Comment.Value,
            rating.Product.Name.Value);

        return response;
    }

    public async Task<Result<IEnumerable<RatingResponse>>> Handle(
        GetRatingsByProductQuery request,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(
            request.ProductId, 
            cancellationToken);

        if (product is null)
        {
            return Result.Failure<IEnumerable<RatingResponse>>(
                DomainErrors.Rating.ProductNotFound(
                    request.ProductId));
        }

        IEnumerable<RatingResponse> responses = product.Ratings
            .Select(rating => new RatingResponse(
                rating.Id,
                rating.Score,
                rating.Comment.Value,
                rating.Product.Name.Value));

        return Result.Success(responses);
    }
}
