using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Enums;

namespace VM.Application.Segregation.Ratings.Commands.AddRating;

public sealed record AddRatingCommand(
    Score Score,
    string Comment,
    Guid ProductId) : ICommand<Guid>;