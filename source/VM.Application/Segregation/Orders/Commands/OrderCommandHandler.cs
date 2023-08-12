using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Orders.Commands.Create;
using VM.Application.Segregation.Orders.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Orders.Commands;

internal sealed class OrderCommandHandler :
    ICommandHandler<CreateOrderCommand, Guid>,
    ICommandHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShippingRepository _shippingRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public OrderCommandHandler(
        IOrderRepository orderRepository,
        IShippingRepository shippingRepository,
        IPaymentRepository paymentRepository,
        IUserRepository userRepository,
        IUnitOfWork uow)
    {
        _orderRepository = orderRepository;
        _shippingRepository = shippingRepository;
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        Result<Name> methodResult = Name.Create(request.Method);
        Result<LongText> addressResult = LongText.Create(
            request.Address);
        Result<Amount> costResult = Amount.Create(request.Cost),
            amountResult = Amount.Create(request.Amount);

        Result<Amount> totalAmount = Amount.Create(
            costResult.Value.Value +
            amountResult.Value.Value);

        var shipping = Shipping.Create(
            Guid.NewGuid(),
            addressResult.Value,
            costResult.Value);

        var payment = Payment.Create(
            Guid.NewGuid(),
            methodResult.Value,
            amountResult.Value);

        var user = await _userRepository.GetByIdAsync(
            request.UserId, 
            cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(DomainErrors.Order.UserNotFound(
                request.UserId));
        }

        var order = Order.Create(
            Guid.NewGuid(),
            totalAmount.Value,
            user,
            shipping,
            payment);

        payment.SetOrder(order);
        shipping.SetOrder(order);

        order.AddEstablishedRelationships(payment, shipping);

        user.AddNewOrder(order);

        await _orderRepository.AddAsync(order, cancellationToken);
        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _shippingRepository.AddAsync(shipping, cancellationToken);

        _userRepository.Update(user);

        await _uow.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    public async Task<Result> Handle(
        UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);

        if (order is null)
        {
            return Result.Failure(DomainErrors.Order.NotFound(
                request.Id));
        }

        order.ChangeStatus(request.Cancel);

        _orderRepository.Update(order);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
