using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Orders.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Orders.Queries;

internal sealed class OrderQueryHandler :
    IQueryHandler<GetOrderByIdQuery, OrderResponse>,
    IQueryHandler<GetOrdersByUserQuery, IEnumerable<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public OrderQueryHandler(
        IOrderRepository orderRepository,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<OrderResponse>> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(
            request.Id, cancellationToken);

        if (order is null)
        {
            return Result.Failure<OrderResponse>(
                DomainErrors.Order.NotFound(request.Id));
        }

        var response = new OrderResponse(
            order.Id,
            order.TotalAmount.Value,

            new OrderUserResponse(
                order.UserId,
                $"{order.User.Firstname}" + 
                $"{order.User.Lastname}"),
            
            new OrderShippingResponse(
                order.Shipping.Id,
                order.Shipping.Address.Value,
                order.Shipping.Cost.Value),
            
            new OrderPaymentResponse(
                order.Payment.Id,
                order.Payment.Method.Value,
                order.Payment.Amount.Value));

        return response;
    }

    public async Task<Result<IEnumerable<OrderResponse>>> Handle(
        GetOrdersByUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            request.UserId, 
            cancellationToken);

        if (user is null)
        {
            return Result.Failure<IEnumerable<OrderResponse>>(
                DomainErrors.Order.UserNotFound(request.UserId));
        }
        
        IEnumerable<Order> orders = await _orderRepository
            .GetByConditionAsync(
                   order => order.User == user, 
                   cancellationToken);

        if (orders is null || !orders.Any())
        {
            return Array.Empty<OrderResponse>();
        }

        IEnumerable<OrderResponse> responses = 
            orders
        .Select(order => new OrderResponse(
                order.Id,
                order.TotalAmount.Value,

                new OrderUserResponse(
                    order.UserId,
                    $"{order.User.Firstname}" + 
                    $"{order.User.Lastname}"),

                new OrderShippingResponse(
                    order.Shipping.Id,
                    order.Shipping.Address.Value,
                    order.Shipping.Cost.Value),

                new OrderPaymentResponse(
                    order.Payment.Id,
                    order.Payment.Method.Value,
                    order.Payment.Amount.Value)));

        return Result.Success(responses);
    }
}
