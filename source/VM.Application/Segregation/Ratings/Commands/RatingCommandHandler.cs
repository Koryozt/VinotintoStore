using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Ratings.Commands.AddRating;
using VM.Application.Segregation.Ratings.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Ratings.Commands;

internal sealed class RatingCommandHandler :
    ICommandHandler<AddRatingCommand, Guid>,
    ICommandHandler<UpdateRatingCommand>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public RatingCommandHandler(
        IRatingRepository ratingRepository,
        IProductRepository productRepository,
        IUnitOfWork uow)
    {
        _ratingRepository = ratingRepository;
        _productRepository = productRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(
        AddRatingCommand request,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
        {
            return Result.Failure<Guid>(
                DomainErrors.Rating.ProductNotFound(
                    request.ProductId));
        }

        Result<LongText> commentResult = LongText.Create(
            request.Comment);

        var rating = Rating.Create(
            Guid.NewGuid(),
            request.Score,
            commentResult.Value,
            product);

        await _ratingRepository.AddAsync(
            rating,
            cancellationToken);

        product.AddRating(rating);

        _productRepository.Update(product);

        await _uow.SaveChangesAsync(cancellationToken);

        return rating.Id;
    }

    public async Task<Result> Handle(
        UpdateRatingCommand request,
        CancellationToken cancellationToken)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (rating is null)
        {
            return Result.Failure(
                DomainErrors.Rating.NotFound(request.Id));
        }

        Result<LongText> commentResult = LongText.Create(
            request.Comment);

        rating.ModifyCommentAndScoring(
            commentResult.Value,
            request.Score);

        _ratingRepository.Update(rating);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
