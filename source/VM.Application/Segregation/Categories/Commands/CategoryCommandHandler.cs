using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Categories.Commands.AddProduct;
using VM.Application.Segregation.Categories.Commands.Create;
using VM.Application.Segregation.Categories.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Categories.Commands;

internal sealed class CategoryCommandHandler :
    ICommandHandler<CreateCategoryCommand, Guid>,
    ICommandHandler<AddProductToCategoryCommand>,
    ICommandHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public CategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository,
        IUnitOfWork uow)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);

        if ((await _categoryRepository.GetByConditionAsync(
            c => c.Name == nameResult.Value, cancellationToken)) != null)
        {
            return Result.Failure<Guid>(
                DomainErrors.Category.AlreadyExisting(request.Name));
        }

        var category = Category.Create(Guid.NewGuid(), nameResult.Value);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return category.Id;
    }

    public async Task<Result> Handle(
        AddProductToCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(
            request.ProductId, 
            cancellationToken);

        var category = await _categoryRepository.GetByIdAsync(
            request.CategoryId,
            cancellationToken);

        if (product is null)
        {
            return Result.Failure(DomainErrors.Category.ProductNotFound(
                request.ProductId));
        }

        if (category is null)
        {
            return Result.Failure(DomainErrors.Category.NotFound(
                request.CategoryId.ToString()));
        }

        product.AddCategory(category);
        category.AddProduct(product);

        _categoryRepository.Update(category);
        _productRepository.Update(product);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);

        var category = await _categoryRepository.GetByIdAsync(
            request.Id, cancellationToken);

        if (category is null)
        {
            return Result.Failure(DomainErrors.Category.NotFound(
                request.Id.ToString()));
        }

        category.ChangeName(nameResult.Value);

        _categoryRepository.Update(category);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
