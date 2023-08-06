using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Segregation.Categories.Queries;
using VM.Application.Segregation.Ratings.Queries;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Products.Queries;

public sealed record ProductResponse(
    string Name,
    string Description,
    double Price,
    int Stock,
    IReadOnlyCollection<CategoryResponse> Categories,
    IReadOnlyCollection<RatingResponse> Ratings);
