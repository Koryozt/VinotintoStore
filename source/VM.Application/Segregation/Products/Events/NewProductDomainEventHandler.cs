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

namespace VM.Application.Segregation.Products.Events;

internal sealed class NewProductDomainEventHandler :
    IDomainEventHandler<NewProductDomainEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly IEmailService _emailService;

    public NewProductDomainEventHandler(
        IProductRepository productRepository,
        IEmailService emailService)
    {
        _productRepository = productRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        NewProductDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(
            notification.ProductId,
            cancellationToken);

        if (product is null)
        {
            return;
        }

        await _emailService.SendProductCreatedEmailAsync(
            product,
            cancellationToken);
    }
}