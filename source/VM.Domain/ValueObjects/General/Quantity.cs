using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Errors;
using VM.Domain.Primitives;
using VM.Domain.Shared;

namespace VM.Domain.ValueObjects.General;

public sealed class Quantity : ValueObject
{
    public int Value { get; set; }
    public const int MinValue = 0;
    public const int MaxValue = 100_000;

    private Quantity(int value)
    {
        Value = value;
    }

    public static Result<Quantity> Create(int value) =>
        Result.Create(value)
            .Ensure(x => x >= MinValue,
                DomainErrors.Quantity.NegativeValue)
            .Ensure(x => x <= MaxValue,
                DomainErrors.Quantity.TooLong)
            .Map(x => new Quantity(x));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
