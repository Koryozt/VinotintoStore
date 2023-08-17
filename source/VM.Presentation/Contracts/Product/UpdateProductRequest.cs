using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.Product;

public sealed record UpdateProductRequest(
    string Name,
    string Description,
    double Price,
    int Stock);