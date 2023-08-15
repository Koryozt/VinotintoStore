using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Enums;

namespace VM.Presentation.Contracts.Rating;

public sealed record AddRatingRequest(
    Score Score,
    string Comment,
    Guid ProductId);