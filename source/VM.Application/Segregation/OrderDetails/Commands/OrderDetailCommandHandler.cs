using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.OrderDetails.Commands.AddDetail;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.OrderDetails.Commands;

internal sealed class OrderDetailCommandHandler :
    ICommandHandler<AddOrderDetailCommand, Guid>
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;

    public OrderDetailCommandHandler(
        IOrderDetailRepository orderDetailRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUnitOfWork uow)
    {
        _orderDetailRepository = orderDetailRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(
        AddOrderDetailCommand request,
        CancellationToken cancellationToken)
    {
        Result<Quantity> quantityResult = Quantity.Create(
            request.Quantity);
        Result<Amount> priceResult = Amount.Create(
            request.Price);

        Product? product = await _productRepository.GetByIdAsync(
            request.ProductId,
            cancellationToken);

        Order? order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            cancellationToken);

        if (product is null)
        {
            return Result.Failure<Guid>(
                DomainErrors.OrderDetail.ProductNotFound(
                request.ProductId));
        }

        if (order is null)
        {
            return Result.Failure<Guid>(
                DomainErrors.OrderDetail.OrderNotFound(
                request.OrderId));
        }

        int remaining = product.Stock.Value - 
            quantityResult.Value.Value;

        if (product.Stock.Value == 0 || 
            remaining < 0)
        {
            return Result.Failure<Guid>(
                DomainErrors.OrderDetail.ProductOutOfStock);
        }

        var orderDetail = OrderDetail.Create(
            Guid.NewGuid(),
            quantityResult.Value,
            priceResult.Value,
            product,
            order);

        await _orderDetailRepository.AddAsync(
            orderDetail, 
            cancellationToken);

        product.AddOrderDetail(orderDetail, remaining);
        order.AddOrderDetail(orderDetail);

        _productRepository.Update(product);
        _orderRepository.Update(order);

        await _uow.SaveChangesAsync(cancellationToken);

        return orderDetail.Id;
    }
}
