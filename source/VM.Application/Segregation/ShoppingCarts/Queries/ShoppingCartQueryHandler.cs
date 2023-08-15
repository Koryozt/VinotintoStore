using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Queries;
using VM.Application.Segregation.ShoppingCarts.Queries.Statements;
using VM.Application.Segregation.Users.Queries;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.ShoppingCarts.Queries;

internal sealed class ShoppingCartQueryHandler :
    IQueryHandler<GetShoppingCartByIdQuery, ShoppingCartResponse>,
    IQueryHandler<GetShoppingCartByUserQuery, ShoppingCartResponse>
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartItemRepository _cartItemRepository;

    public ShoppingCartQueryHandler(
        IShoppingCartRepository shoppingCartRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        ICartItemRepository cartItemRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task<Result<ShoppingCartResponse>> Handle(
        GetShoppingCartByIdQuery request,
        CancellationToken cancellationToken)
    {
        ShoppingCart? shoppingCart = await _shoppingCartRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (shoppingCart is null)
        {
            return Result.Failure<ShoppingCartResponse>(
                DomainErrors.ShoppingCart.NotFound(request.Id));
        }

        IEnumerable<CartItem> items = await _cartItemRepository.GetByConditionAsync(
            i => i.ShoppingCartId == shoppingCart.Id,
            cancellationToken);

        List<ShoppingCartItemResponse> itemsResponse = new();

        foreach(CartItem item in shoppingCart.CartItems)
        {
            Product? itemProduct = await _productRepository
                .GetByIdAsync(
                    item.ProductId,
                    cancellationToken);

            var cartItem = new ShoppingCartItemResponse(
                item.Id,
                item.Quantity.Value,
                item.TotalPrice.Value,
                new CartItemProductResponse(
                    itemProduct.Id,
                    itemProduct.Name.Value,
                    itemProduct.Description.Value,
                    itemProduct.Price.Value,
                    itemProduct.Stock.Value));

            itemsResponse.Add(cartItem);
        }

        User? user = await _userRepository.GetByIdAsync(
            shoppingCart.UserId,
            cancellationToken);

        ShoppingCartResponse response = new(
            shoppingCart.Id,
            new ShoppingCartUserResponse(
                user.Id,
                user.Firstname.Value,
                user.Lastname.Value
                ),
            itemsResponse);

        return response;
    }

    public async Task<Result<ShoppingCartResponse>> Handle(
        GetShoppingCartByUserQuery request,
        CancellationToken cancellationToken)
    {

        ShoppingCart? shoppingCart = (await _shoppingCartRepository
            .GetByConditionAsync(
                cart => request.UserId == cart.UserId, 
                cancellationToken)).FirstOrDefault();

        if (shoppingCart is null)
        {
            return Result.Failure<ShoppingCartResponse>(
                DomainErrors.ShoppingCart.UserNotFound(
                    request.UserId));
        }

        IEnumerable<CartItem> items = await _cartItemRepository.GetByConditionAsync(
            i => i.ShoppingCartId == shoppingCart.Id,
            cancellationToken);

        List<ShoppingCartItemResponse> itemsResponse = new();

        foreach (CartItem item in shoppingCart.CartItems)
        {
            Product? itemProduct = await _productRepository
                .GetByIdAsync(
                    item.ProductId,
                    cancellationToken);

            var cartItem = new ShoppingCartItemResponse(
                item.Id,
                item.Quantity.Value,
                item.TotalPrice.Value,
                new CartItemProductResponse(
                    itemProduct.Id,
                    itemProduct.Name.Value,
                    itemProduct.Description.Value,
                    itemProduct.Price.Value,
                    itemProduct.Stock.Value));

            itemsResponse.Add(cartItem);
        }

        User? user = await _userRepository.GetByIdAsync(
            shoppingCart.UserId,
            cancellationToken);

        ShoppingCartResponse response = new(
            shoppingCart.Id,
            new ShoppingCartUserResponse(
                user.Id,
                user.Firstname.Value,
                user.Lastname.Value
                ),
            itemsResponse);

        return response;
    }
}
