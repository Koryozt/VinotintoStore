using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Enums;

namespace VM.Application.Segregation.Products.Queries;

public sealed record ProductRatingResponse(
    Guid Id,
    Score Score,
    string Comment);