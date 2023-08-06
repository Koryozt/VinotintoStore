using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Users.Commands.Create;
using VM.Application.Segregation.Users.Commands.Login;
using VM.Application.Segregation.Users.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Users.Commands;

internal sealed class UserCommandHandler :
    ICommandHandler<CreateUserCommand, Guid>,
    ICommandHandler<LoginCommand, string>,
    ICommandHandler<UpdateUserCommand>
{
    public Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
