using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.CartItems.Queries;

internal sealed class CartItemQueryHandler :
    IQueryHandler<GetCartItemByIdQuery, CartItemResponse>,
    IQueryHandler<GetCartItemByProductQuery, CartItemResponse>,
    IQueryHandler<GetCartItemsByShopingCartQuery, IEnumerable<CartItemResponse>>
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemQueryHandler(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<Result<CartItemResponse>> Handle(
        GetCartItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _cartItemRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (item is null)
        {
            return Result.Failure<CartItemResponse>(
                DomainErrors.CartItem.NotFound(request.Id));
        }

        CartItemResponse response = new(
            item.Id,
            item.Quantity.Value,
            item.TotalPrice.Value,
            new CartItemProductResponse(
                item.ProductId,
                item.Product.Name.Value,
                item.Product.Description.Value,
                item.Product.Price.Value,
                item.Product.Stock.Value),
            new CartItemShoppingCartResponse(
            item.ShoppingCartId)
            );

        return response;
    }

    public async Task<Result<CartItemResponse>> Handle(
        GetCartItemByProductQuery request,
        CancellationToken cancellationToken)
    {
        var item = (await _cartItemRepository.GetByConditionAsync(
            cartItem => request.ProductId == cartItem.ProductId,
            cancellationToken)).FirstOrDefault();

        if (item is null)
        {
            return Result.Failure<CartItemResponse>(
                DomainErrors.CartItem.ProductNotFound(request.ProductId));
        }

        CartItemResponse response = new(
            item.Id,
            item.Quantity.Value,
            item.TotalPrice.Value,
            new CartItemProductResponse(
                item.ProductId,
                item.Product.Name.Value,
                item.Product.Description.Value,
                item.Product.Price.Value,
                item.Product.Stock.Value),
            new CartItemShoppingCartResponse(
            item.ShoppingCartId)
            );

        return response;
    }

    public async Task<Result<IEnumerable<CartItemResponse>>> Handle(
        GetCartItemsByShopingCartQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _cartItemRepository.GetByConditionAsync(
            cartItem => request.ShopingCartId == cartItem.ShoppingCartId,
            cancellationToken);

        if (items is null)
        {
            return Result.Failure<IEnumerable<CartItemResponse>>(
                DomainErrors.CartItem.ShoppingCartNotFound(request.ShopingCartId));
        }

        IEnumerable<CartItemResponse> response =
            items
                .Select(item =>
                    new CartItemResponse(
                        item.Id,
                        item.Quantity.Value,
                        item.TotalPrice.Value,
                        new CartItemProductResponse(
                            item.ProductId,
                            item.Product.Name.Value,
                            item.Product.Description.Value,
                            item.Product.Price.Value,
                            item.Product.Stock.Value),
                        new CartItemShoppingCartResponse(
                        item.ShoppingCartId)
                 ));

        return Result.Success(response);
    }
}
