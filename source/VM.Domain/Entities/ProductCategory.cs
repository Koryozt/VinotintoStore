using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Domain.Entities;

public sealed class ProductCategory
{
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
}
