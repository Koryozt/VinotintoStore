using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Application.Segregation.Categories.Queries;

public sealed record ProductCategoryResponse(
    Guid Id,
    string Name,
    string Description,
    double Price,
    int Stock);