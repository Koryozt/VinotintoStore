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

    public UserQueryHandler(
        IUserRepository userRepository,
        IShoppingCartRepository shoppingCartRepository)
    {
        _userRepository = userRepository;
        _shoppingCartRepository = shoppingCartRepository;
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

        UserResponse response = new(
            user.Id,
            user.Firstname.Value,
            user.Lastname.Value,
            
            new UserShoppingCartResponse(
                user.ShoppingCart.Id,
                user.ShoppingCart.CartItems
                    .Select(item => new ShoppingCartItemResponse(
                        item.Id,
                        item.Quantity.Value,
                        item.TotalPrice.Value,
                        new CartItemProductResponse(
                            item.ProductId,
                            item.Product.Name.Value,
                            item.Product.Description.Value,
                            item.Product.Price.Value,
                            item.Product.Stock.Value)))));

        return response;
    }
}
