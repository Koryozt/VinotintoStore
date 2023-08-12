using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.CartItems.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.CartItems.Queries;

internal sealed class CartItemQueryHandler :
    IQueryHandler<GetCartItemByIdQuery, CartItemResponse>,
    IQueryHandler<GetCartItemByProductQuery, CartItemResponse>,
    IQueryHandler<GetCartItemsByShopingCartQuery, IEnumerable<CartItemResponse>>
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductRepository _productRepository;

    public CartItemQueryHandler(
        ICartItemRepository cartItemRepository,
        IProductRepository productRepository)
    {
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<CartItemResponse>> Handle(
        GetCartItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        CartItem? item = await _cartItemRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (item is null)
        {
            return Result.Failure<CartItemResponse>(
                DomainErrors.CartItem.NotFound(request.Id));
        }

        Product? product = await _productRepository.GetByIdAsync(
            item.ProductId,
            cancellationToken);

        CartItemResponse response = new(
            item.Id,
            item.Quantity.Value,
            item.TotalPrice.Value,
            new CartItemProductResponse(
                product.Id,
                product.Name.Value,
                product.Description.Value,
                product.Price.Value,
                product.Stock.Value),
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

        Product? product = await _productRepository.GetByIdAsync(
            item.ProductId,
            cancellationToken);

        CartItemResponse response = new(
            item.Id,
            item.Quantity.Value,
            item.TotalPrice.Value,
            new CartItemProductResponse(
                product.Id,
                product.Name.Value,
                product.Description.Value,
                product.Price.Value,
                product.Stock.Value),
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

        List<CartItemResponse> responses = new();

        foreach(CartItem item in items)
        {
            Product? product = await _productRepository.GetByIdAsync(
                item.ProductId,
                cancellationToken);

            CartItemResponse response = new(
                item.Id,
                item.Quantity.Value,
                item.TotalPrice.Value,
                new CartItemProductResponse(
                    product.Id,
                    product.Name.Value,
                    product.Description.Value,
                    product.Price.Value,
                    product.Stock.Value),
                new CartItemShoppingCartResponse(
                    item.ShoppingCartId)
                );

            responses.Add(response);
        }

        return Result.Success(responses.AsEnumerable());
    }
}
