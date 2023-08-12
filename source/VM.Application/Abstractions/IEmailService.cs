using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Application.Abstract;
public interface IEmailService
{
    Task SendWelcomeEmailAsync(
        User user,
        CancellationToken cancellationToken = default);
    Task SendUpdatedInformationEmailAsync(
        User user,
        CancellationToken cancellationToken = default);
    Task SendOrderConfirmationEmailAsync(
        Order order,
        CancellationToken cancellationToken = default);
    Task SendOrderStatusChangedEmailAsync(
        Order order,
        CancellationToken cancellationToken = default);
    Task SendProductCreatedEmailAsync(
        Product product,
        CancellationToken cancellationToken = default);
    Task SendProductModifiedEmailAsync(
        Product product,
        CancellationToken cancellationToken = default);
}
