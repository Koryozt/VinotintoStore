using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Categories.Queries.Statements;
using VM.Domain.Shared;

namespace VM.Application.Segregation.Categories.Queries;

internal sealed class CategoryQueryHandler :
    IQueryHandler<GetCategoryByIdQuery, CategoryResponse>,
    IQueryHandler<GetCategoryByNameQuery, CategoryResponse>,
    IQueryHandler<GetCategoriesByProductQuery, IEnumerable<CategoryResponse>>
{
    public Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CategoryResponse>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<CategoryResponse>>> Handle(GetCategoriesByProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
