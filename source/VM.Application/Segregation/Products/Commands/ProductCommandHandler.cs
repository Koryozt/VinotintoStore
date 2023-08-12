using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Products.Commands.Create;
using VM.Application.Segregation.Products.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Products.Commands;

internal sealed class ProductCommandHandler :
    ICommandHandler<CreateProductCommand, Guid>,
    ICommandHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public ProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork uow)
    {
        _productRepository = productRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Photo))
        {
            return Result.Failure<Guid>(
                DomainErrors.Product.NoPhotoProvided);
        }

        Result<Name> nameResult = Name.Create(request.Name);
        Result<LongText> descResult = LongText.Create(
            request.Description);
        Result<Amount> priceResult = Amount.Create(
            request.Price);
        Result<Quantity> stockResult = Quantity.Create(
            request.Stock);

        var product = Product.Create(
            Guid.NewGuid(),
            request.Photo,
            nameResult.Value,
            descResult.Value,
            priceResult.Value,
            stockResult.Value
            );

        await _productRepository.AddAsync(
            product, 
            cancellationToken);

        await _uow.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public async Task<Result> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (product is null) 
        {
            return Result.Failure(
                DomainErrors.Product.NotFound(request.Id.ToString()));
        }

        Result<Name> nameResult = Name.Create(request.Name);
        Result<LongText> descResult = LongText.Create(
            request.Description);
        Result<Amount> priceResult = Amount.Create(
            request.Price);
        Result<Quantity> stockResult = Quantity.Create(
            request.Stock);

        product.ChangeData(
            nameResult.Value,
            descResult.Value,
            priceResult.Value,
            stockResult.Value);

        _productRepository.Update(product);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
