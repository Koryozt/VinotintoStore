using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.OrderDetails.Commands.AddDetail;
using VM.Domain.Shared;

namespace VM.Application.Segregation.OrderDetails.Commands;

internal sealed class OrderDetailCommandHandler :
    ICommandHandler<AddOrderDetailCommand, Guid>
{
    public Task<Result<Guid>> Handle(AddOrderDetailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
