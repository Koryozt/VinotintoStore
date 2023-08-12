using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Categories.Queries;

public sealed record CategoryResponse(
    Guid Id,
    string Name,
    IReadOnlyCollection<ProductCategoryResponse> Products);