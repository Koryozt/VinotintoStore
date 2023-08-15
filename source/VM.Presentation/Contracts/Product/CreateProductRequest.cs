using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.Product;

public sealed record CreateProductRequest(
    string Photo,
    string Name,
    string Description,
    double Price,
    int Stock);
