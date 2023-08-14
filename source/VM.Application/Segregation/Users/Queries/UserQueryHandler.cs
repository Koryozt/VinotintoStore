using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Queries;
using VM.Application.Segregation.ShoppingCarts.Queries;
using VM.Application.Segregation.Users.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Users.Queries;

internal sealed class UserQueryHandler :
    IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public UserQueryHandler(
        IUserRepository userRepository,
        IShoppingCartRepository shoppingCartRepository,
        IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<UserResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(DomainErrors.User.NotFound(
                request.Id));
        }

        ShoppingCart? cart = await _shoppingCartRepository.GetByIdAsync(
            user.ShoppingCartId,
            cancellationToken);

        List<ShoppingCartItemResponse> parsedCartItemResponse = new();

        foreach(CartItem item in cart.CartItems)
        {
            Product? product = await _productRepository.GetByIdAsync(
                item.ProductId,
                cancellationToken);

            var shoppingCartItem = new ShoppingCartItemResponse(
                        item.Id,
                        item.Quantity.Value,
                        item.TotalPrice.Value,
                        new CartItemProductResponse(
                            product.Id,
                            product.Name.Value,
                            product.Description.Value,
                            product.Price.Value,
                            product.Stock.Value));

            parsedCartItemResponse.Add(shoppingCartItem);
        }

        UserResponse response = new(
            user.Id,
            user.Firstname.Value,
            user.Lastname.Value,

            new UserShoppingCartResponse(
                cart.Id,
                parsedCartItemResponse));

        return response;
    }
}
