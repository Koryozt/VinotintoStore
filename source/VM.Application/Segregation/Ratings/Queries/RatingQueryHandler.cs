using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Ratings.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Ratings.Queries;

internal sealed class RatingQueryHandler :
    IQueryHandler<GetRatingByIdQuery, RatingResponse>,
    IQueryHandler<GetRatingsByProductQuery, IEnumerable<RatingResponse>>
{
    public Task<Result<RatingResponse>> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<RatingResponse>>> Handle(GetRatingsByProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
