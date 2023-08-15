using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Commands.Create;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.CartItems.Commands;

internal sealed class CartItemCommandHandler :
    ICommandHandler<AddCartItemCommand, Guid>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public CartItemCommandHandler(
        IShoppingCartRepository shoppingCartRepository,
        IProductRepository productRepository,
        IUnitOfWork uow,
        ICartItemRepository cartItemRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
        _uow = uow;
        _cartItemRepository = cartItemRepository;
    }

    public async Task<Result<Guid>> Handle(
        AddCartItemCommand request,
        CancellationToken cancellationToken)
    {
        Result<Amount> totalPriceResult = Amount.Create(request.TotalPrice);
        Result<Quantity> quantityResult = Quantity.Create(request.Quantity);

        var cartProduct = await _productRepository.GetByIdAsync(
            request.ProductId, 
            cancellationToken);

        var shoppingCart = await _shoppingCartRepository.GetByIdAsync(
            request.ShoppingCartId, 
            cancellationToken);

        if (cartProduct is null)
        {
            return Result.Failure<Guid>(
                DomainErrors.CartItem.ProductNotFound(request.ProductId));
        }

        if (shoppingCart is null)
        {
            return Result.Failure<Guid>(
                DomainErrors
                .CartItem.ShoppingCartNotFound(request.ShoppingCartId));
        }

        var cartItem = CartItem.AddItemToShoppingCart(
            Guid.NewGuid(),
            totalPriceResult.Value,
            quantityResult.Value,
            cartProduct,
            shoppingCart);

        await _cartItemRepository.AddAsync(cartItem, cancellationToken);

        shoppingCart.AddNewItem(cartItem);

        _shoppingCartRepository.Update(shoppingCart);

        await _uow.SaveChangesAsync(cancellationToken);

        return cartItem.Id;
    }
}
