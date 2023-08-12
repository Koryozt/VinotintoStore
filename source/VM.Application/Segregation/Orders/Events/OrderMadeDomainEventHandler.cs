using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstract;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Abstractions;
using VM.Domain.DomainEvents;
using VM.Domain.Entities;

namespace VM.Application.Segregation.Orders.Events;
internal sealed class OrderMadeDomainEventHandler : 
    IDomainEventHandler<OrderMadeDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;

    public OrderMadeDomainEventHandler(
        IOrderRepository orderRepository,
        IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        OrderMadeDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(
            notification.OrderId,
            cancellationToken);

        if (order is null)
        {
            return;
        }

        await _emailService.SendOrderConfirmationEmailAsync(
            order, 
            cancellationToken);
    }
}
