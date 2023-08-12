using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.OrderDetails.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.OrderDetails.Queries;

internal sealed class OrderDetailsQueryHandler :
    IQueryHandler<GetOrderDetailByIdQuery, OrderDetailResponse>,
    IQueryHandler<GetOrderDetailsByOrderQuery, IEnumerable<OrderDetailResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;  
    private readonly IOrderDetailRepository _orderDetailRepository;

    public OrderDetailsQueryHandler(
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<OrderDetailResponse>> Handle(
        GetOrderDetailByIdQuery request,
        CancellationToken cancellationToken)
    {
        OrderDetail? orderDetail = await _orderDetailRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (orderDetail is null)
        {
            return Result.Failure<OrderDetailResponse>(DomainErrors.OrderDetail
                .NotFound(request.Id));
        }

        Product? product = await _productRepository.GetByIdAsync(
            orderDetail.ProductId,
            cancellationToken);

        Order? order = await _orderRepository.GetByIdAsync(
            orderDetail.OrderId,
            cancellationToken);

        OrderDetailResponse response = new(
            orderDetail.Id,
            orderDetail.Quantity,
            orderDetail.Price,
            new OrderDetailProductResponse(
                product.Id,
                product.Name.Value,
                product.Price.Value,
                product.Stock.Value),
            new OrderDetailOrderResponse(
                order.Id,
                order.TotalAmount.Value,
                new OrderDetailUserResponse(
                    order.UserId)));

        return response;
    }

    public async Task<Result<IEnumerable<OrderDetailResponse>>> Handle(
        GetOrderDetailsByOrderQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(
            request.OrderId, 
            cancellationToken);

        if (order is null)
        {
            return Result.Failure<IEnumerable<OrderDetailResponse>>(
                DomainErrors.OrderDetail.OrderNotFound(request.OrderId));
        }

        List<OrderDetailResponse> responses = new();

        foreach(OrderDetail orderDetail in order.OrderDetails)
        {
            Product? product = await _productRepository.GetByIdAsync(
                orderDetail.ProductId,
                cancellationToken);

            OrderDetailResponse response = new(
                orderDetail.Id,
                orderDetail.Quantity,
                orderDetail.Price,
                new OrderDetailProductResponse(
                    product.Id,
                    product.Name.Value,
                    product.Price.Value,
                    product.Stock.Value),
                new OrderDetailOrderResponse(
                    order.Id,
                    order.TotalAmount.Value,
                    new OrderDetailUserResponse(
                        order.UserId)));

            responses.Add(response);
        }

        return Result.Success(responses.AsEnumerable());
    }
}
