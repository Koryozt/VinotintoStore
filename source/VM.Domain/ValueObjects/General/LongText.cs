using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Errors;
using VM.Domain.Primitives;
using VM.Domain.Shared;

namespace VM.Domain.ValueObjects.General;

public sealed class LongText : ValueObject
{
    public const int MaxLength = 5000;
    public string Value { get; private set; }

    private LongText(string value)
    {
        Value = value;
    }

    private LongText()
    {

    }

    public static Result<LongText> Create(string text) =>
        Result.Create(text)
            .Ensure(x => !string.IsNullOrEmpty(text),
                DomainErrors.LongText.Empty)
            .Ensure(x => x.Length < MaxLength,
                DomainErrors.LongText.TooLong)
            .Map(x => new LongText(x));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}