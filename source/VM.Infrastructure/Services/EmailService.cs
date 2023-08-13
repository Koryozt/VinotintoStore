using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstract;
using VM.Domain.Entities;

namespace VM.Infrastructure.Services;

internal sealed class EmailService : IEmailService
{
    public Task SendOrderConfirmationEmailAsync(Order order, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendOrderStatusChangedEmailAsync(Order order, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendProductCreatedEmailAsync(Product product, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendProductModifiedEmailAsync(Product product, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendUpdatedInformationEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
