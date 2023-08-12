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
    private readonly IShippingRepository _shippingRepository;
    private readonly IPaymentRepository _paymentRepository;

    public OrderQueryHandler(
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        IShippingRepository shippingrepository,
        IPaymentRepository paymentRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _shippingRepository = shippingrepository;
        _paymentRepository = paymentRepository;
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

        Shipping? shipping = await _shippingRepository
            .GetByIdAsync(
                order.ShippingId,
                cancellationToken);

        Payment? payment = await _paymentRepository
            .GetByIdAsync(
                order.PaymentId,
                cancellationToken);

        User? user = await _userRepository.GetByIdAsync(
            order.UserId,
            cancellationToken);

        var response = new OrderResponse(
            order.Id,
            order.TotalAmount.Value,

            new OrderUserResponse(
                user.Id,
                $"{user.Firstname}" + 
                $"{user.Lastname}"),
            
            new OrderShippingResponse(
                shipping.Id,
                shipping.Address.Value,
                shipping.Cost.Value),
            
            new OrderPaymentResponse(
                payment.Id,
                payment.Method.Value,
                payment.Amount.Value));

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

        List<OrderResponse> responses = new();

        foreach(Order order in orders)
        {
            Shipping? shipping = await _shippingRepository
                .GetByIdAsync(
                    order.ShippingId,
                    cancellationToken);

            Payment? payment = await _paymentRepository
                .GetByIdAsync(
                    order.PaymentId,
                    cancellationToken);

            var response = new OrderResponse(
                order.Id,
                order.TotalAmount.Value,

                new OrderUserResponse(
                    user.Id,
                    $"{user.Firstname}" +
                    $"{user.Lastname}"),

                new OrderShippingResponse(
                    shipping.Id,
                    shipping.Address.Value,
                    shipping.Cost.Value),

                new OrderPaymentResponse(
                    payment.Id,
                    payment.Method.Value,
                    payment.Amount.Value));

            responses.Add(response);
        }

        return Result.Success(responses.AsEnumerable());
    }
}
