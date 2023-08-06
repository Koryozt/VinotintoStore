using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Ratings.Commands.AddRating;
using VM.Application.Segregation.Ratings.Commands.Update;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Ratings.Commands;

internal sealed class RatingCommandHandler :
    ICommandHandler<AddRatingCommand, Guid>,
    ICommandHandler<UpdateRatingCommand>
{
    public Task<Result<Guid>> Handle(AddRatingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
