using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.OrderDetails.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.OrderDetails.Queries;

internal sealed class OrderDetailsQueryHandler :
    IQueryHandler<GetOrderDetailByIdQuery, OrderDetailResponse>,
    IQueryHandler<GetOrderDetailsByOrderQuery, IEnumerable<OrderDetailResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;

    public OrderDetailsQueryHandler(
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<Result<OrderDetailResponse>> Handle(
        GetOrderDetailByIdQuery request,
        CancellationToken cancellationToken)
    {
        var orderDetail = await _orderDetailRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (orderDetail is null)
        {
            return Result.Failure<OrderDetailResponse>(DomainErrors.OrderDetail
                .NotFound(request.Id));
        }

        OrderDetailResponse response = new(
            orderDetail.Id,
            orderDetail.Quantity,
            orderDetail.Price,
            new OrderDetailProductResponse(
                orderDetail.ProductId,
                orderDetail.Product.Name,
                orderDetail.Product.Price,
                orderDetail.Product.Stock),
            new OrderDetailOrderResponse(
                orderDetail.OrderId,
                orderDetail.Order.TotalAmount,
                new OrderDetailUserResponse(
                    orderDetail.Order.UserId,
                    orderDetail.Order.User.Firstname.Value,
                    orderDetail.Order.User.Lastname.Value
                )
            ));

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

        IEnumerable<OrderDetailResponse> response =
            order.OrderDetails
            .Select(
                orderDetail => new OrderDetailResponse(
                    orderDetail.Id,
                    orderDetail.Quantity,
                    orderDetail.Price,
                    new OrderDetailProductResponse(
                        orderDetail.ProductId,
                        orderDetail.Product.Name,
                        orderDetail.Product.Price,
                        orderDetail.Product.Stock),
                    new OrderDetailOrderResponse(
                        orderDetail.OrderId,
                        orderDetail.Order.TotalAmount,
                        new OrderDetailUserResponse(
                            orderDetail.Order.UserId,
                            orderDetail.Order.User.Firstname.Value,
                            orderDetail.Order.User.Lastname.Value
                ))));

        return Result.Success(response);
    }
}
