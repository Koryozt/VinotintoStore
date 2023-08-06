using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Users.Queries.Statements;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;