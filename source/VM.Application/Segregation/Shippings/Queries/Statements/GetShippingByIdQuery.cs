﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Shippings.Queries.Statements;

public sealed record GetShippingByIdQuery(Guid Id) : IQuery<ShippingResponse>;