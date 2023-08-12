using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Application.Segregation.Orders.Queries;

public sealed record OrderUserResponse(
    Guid Id,
    string Fullname);