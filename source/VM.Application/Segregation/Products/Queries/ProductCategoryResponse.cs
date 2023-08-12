using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Application.Segregation.Products.Queries;

public sealed record ProductCategoryResponse(
    Guid Id,
    string Name);