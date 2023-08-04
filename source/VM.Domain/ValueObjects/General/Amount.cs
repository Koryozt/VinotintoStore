using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Errors;
using VM.Domain.Primitives;
using VM.Domain.Shared;

namespace VM.Domain.ValueObjects.General;

public sealed class Amount : ValueObject
{
    public double Value { get; set; }
    public const double MinValue = 0.00;
    public const double MaxValue = 100_000_000.00;

    private Amount(double value)
    {
        Value = value;
    }

    public static Result<Amount> Create(double value) =>
        Result.Create(value)
            .Ensure(x => x >= MinValue,
                DomainErrors.Numeric.NegativeValue)
            .Ensure(x => x <= MaxValue,
                DomainErrors.Numeric.TooLong)
            .Map(x => new Amount(x));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
