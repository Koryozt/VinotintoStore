using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Shippings.Queries.Statements;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Shippings.Queries;

internal sealed class ShippingQueryHandler :
    IQueryHandler<GetShippingByIdQuery, ShippingResponse>,
    IQueryHandler<GetShippingByOrderQuery, ShippingResponse>
{
    private readonly IShippingRepository _shippingRepository;
    private readonly IOrderRepository _orderRepository;

    public ShippingQueryHandler(
        IShippingRepository shippingRepository,
        IOrderRepository orderRepository)
    {
        _shippingRepository = shippingRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<ShippingResponse>> Handle(
        GetShippingByIdQuery request,
        CancellationToken cancellationToken)
    {
        Shipping? shipping = await _shippingRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (shipping is null)
        {
            return Result.Failure<ShippingResponse>(
                DomainErrors.Shipping.NotFound(request.Id));
        }

        ShippingResponse response = new(
            shipping.Id,
            shipping.Address.Value,
            shipping.Cost.Value,
            shipping.OrderId);

        return response;
    }

    public async Task<Result<ShippingResponse>> Handle(
        GetShippingByOrderQuery request,
        CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            cancellationToken);

        if (order is null)
        {
            return Result.Failure<ShippingResponse>(
                DomainErrors.Shipping.OrderNotFound(request.OrderId));
        }

        ShippingResponse response = new(
            order.Shipping.Id,
            order.Shipping.Address.Value,
            order.Shipping.Cost.Value,
            order.Id);

        return response;
    }
}
