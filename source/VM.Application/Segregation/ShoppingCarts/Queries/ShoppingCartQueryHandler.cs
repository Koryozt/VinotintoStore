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

    public ShoppingCartQueryHandler(
        IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
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

        ShoppingCartResponse response = new(
            shoppingCart.Id,
            
            new ShoppingCartUserResponse(
                shoppingCart.UserId,
                shoppingCart.User.Firstname.Value,
                shoppingCart.User.Lastname.Value
                ),
            
            shoppingCart.CartItems.Select(
                item => new ShoppingCartItemResponse(
                    item.Id,
                    item.Quantity.Value,
                    item.TotalPrice.Value,
                    new CartItemProductResponse(
                        item.ProductId,
                        item.Product.Name.Value,
                        item.Product.Description.Value,
                        item.Product.Price.Value,
                        item.Product.Stock.Value))));

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

        ShoppingCartResponse response = new(
            shoppingCart.Id,

            new ShoppingCartUserResponse(
                shoppingCart.UserId,
                shoppingCart.User.Firstname.Value,
                shoppingCart.User.Lastname.Value
                ),

            shoppingCart.CartItems.Select(
                item => new ShoppingCartItemResponse(
                    item.Id,
                    item.Quantity.Value,
                    item.TotalPrice.Value,
                    new CartItemProductResponse(
                        item.ProductId,
                        item.Product.Name.Value,
                        item.Product.Description.Value,
                        item.Product.Price.Value,
                        item.Product.Stock.Value))));

        return response;
    }
}
