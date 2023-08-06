using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Users.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Users.Queries;

internal sealed class UserQueryHandler :
    IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
