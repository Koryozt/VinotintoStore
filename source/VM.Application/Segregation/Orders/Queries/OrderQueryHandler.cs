using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.OrderDetails.Queries;
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
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IUserRepository _userRepository;
    private readonly IShippingRepository _shippingRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IProductRepository _productRepository;

    public OrderQueryHandler(
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        IShippingRepository shippingrepository,
        IPaymentRepository paymentRepository,
        IOrderDetailRepository orderDetailRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _shippingRepository = shippingrepository;
        _paymentRepository = paymentRepository;
        _orderDetailRepository = orderDetailRepository;
        _productRepository = productRepository;
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

        IEnumerable<OrderDetail> orderDetails = await _orderDetailRepository
            .GetByConditionAsync(
                d => d.OrderId == order.Id,
                cancellationToken);

        List<OrderDetailsResponse> orderDetailsResponse = new();

        foreach (OrderDetail detail in orderDetails)
        {
            Product? product = await _productRepository.GetByIdAsync(
                detail.ProductId,
                cancellationToken);

            var detailResponse = new OrderDetailsResponse(
                detail.Id,
                detail.Quantity.Value,
                detail.Price.Value,
            new OrderDetailProductResponse(
                product.Id,
                product.Name.Value,
                product.Price.Value,
                product.Stock.Value)
            );

            orderDetailsResponse.Add(detailResponse); 
        }

        var response = new OrderResponse(
            order.Id,
            order.TotalAmount.Value,

            new OrderUserResponse(
                user.Id,
                $"{user.Firstname.Value} " + 
                $"{user.Lastname.Value}"),
            
            new OrderShippingResponse(
                shipping.Id,
                shipping.Address.Value,
                shipping.Cost.Value),
            
            new OrderPaymentResponse(
                payment.Id,
                payment.Method.Value,
                payment.Amount.Value),
            orderDetailsResponse
            );

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

            IEnumerable<OrderDetail> orderDetails = await _orderDetailRepository
            .GetByConditionAsync(
                d => d.OrderId == order.Id,
                cancellationToken);

            List<OrderDetailsResponse> orderDetailsResponse = new();

            foreach (OrderDetail detail in orderDetails)
            {
                Product? product = await _productRepository.GetByIdAsync(
                    detail.ProductId,
                    cancellationToken);

                var detailResponse = new OrderDetailsResponse(
                    detail.Id,
                    detail.Quantity.Value,
                    detail.Price.Value,
                new OrderDetailProductResponse(
                    product.Id,
                    product.Name.Value,
                    product.Price.Value,
                    product.Stock.Value)
                );

                orderDetailsResponse.Add(detailResponse);
            }

            var response = new OrderResponse(
                order.Id,
                order.TotalAmount.Value,

                new OrderUserResponse(
                    user.Id,
                    $"{user.Firstname.Value} " +
                    $"{user.Lastname.Value}"),

                new OrderShippingResponse(
                    shipping.Id,
                    shipping.Address.Value,
                    shipping.Cost.Value),

                new OrderPaymentResponse(
                    payment.Id,
                    payment.Method.Value,
                    payment.Amount.Value),
                orderDetailsResponse
                );

            responses.Add(response);
        }

        return Result.Success(responses.AsEnumerable());
    }
}
