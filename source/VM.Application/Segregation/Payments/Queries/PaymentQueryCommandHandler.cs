using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Payments.Queries;

internal sealed class PaymentQueryCommandHandler :
    IQueryHandler<GetPaymentByIdQuery, PaymentResponse>
{
    public Task<Result<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
