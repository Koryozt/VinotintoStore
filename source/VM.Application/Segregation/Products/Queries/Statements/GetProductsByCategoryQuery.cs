using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Products.Queries.Statements;

public sealed record GetProductsByCategoryQuery(string CategoryName) : IQuery<IEnumerable<ProductResponse>>;