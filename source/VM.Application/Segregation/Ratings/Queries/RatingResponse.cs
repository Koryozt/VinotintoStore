using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.Products.Queries;
using VM.Domain.Enums;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Ratings.Queries;

public sealed record RatingResponse(
    Guid Id,
    Score Score,
    string Comment,
    string ProductName);