using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Ratings.Queries.Statements;

public sealed record GetRatingsByProductQuery(Guid ProductId) : IQuery<IEnumerable<RatingResponse>>;