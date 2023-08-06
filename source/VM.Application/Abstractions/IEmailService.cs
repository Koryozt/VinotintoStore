﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Application.Abstract;
public interface IEmailService
{
    Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default);
    Task SendUpdatedInformationEmailAsync(User user, CancellationToken cancellationToken = default);
    Task SendOrderConfirmationEmailAsync(Order order, CancellationToken cancellationToken = default);
    Task SendShippingInformationEmailAsync(Shipping shipping, CancellationToken cancellationToken = default);
}
