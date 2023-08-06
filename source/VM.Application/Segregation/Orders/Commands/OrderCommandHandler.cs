using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Orders.Commands.Create;
using VM.Application.Segregation.Orders.Commands.Update;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Orders.Commands;

internal sealed class OrderCommandHandler :
    ICommandHandler<CreateOrderCommand, Guid>,
    ICommandHandler<UpdateOrderCommand>
{
    public Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
