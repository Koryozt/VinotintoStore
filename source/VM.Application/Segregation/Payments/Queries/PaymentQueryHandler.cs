using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Payments.Queries;

internal sealed class PaymentQueryHandler :
    IQueryHandler<GetPaymentByIdQuery, PaymentResponse>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;

    public PaymentQueryHandler(
        IPaymentRepository paymentRepository,
        IOrderRepository orderRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<PaymentResponse>> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        Payment? payment = await _paymentRepository.GetByIdAsync(
            request.Id, 
            cancellationToken);
        
        if (payment == null)
        {
            return Result.Failure<PaymentResponse>(
                DomainErrors.Payment.NotFound(request.Id));
        }

        Order? order = await _orderRepository.GetByIdAsync(
            payment.OrderId,
            cancellationToken);

        var response = new PaymentResponse(
            payment.Id,
            payment.Method.Value,
            payment.Amount.Value,

            new PaymentOrderResponse(
                order.Id,
                order.TotalAmount.Value));

        return response;
    }
}
