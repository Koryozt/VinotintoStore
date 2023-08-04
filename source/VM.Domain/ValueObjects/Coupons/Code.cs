using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Errors;
using VM.Domain.Primitives;
using VM.Domain.Shared;

namespace VM.Domain.ValueObjects.Coupons;

public sealed class Code : ValueObject
{
    public string Value { get; set; }
    public const int MinLength = 5;
    public const int MaxLength = 15;

    private Code(string value)
    {
        Value = value;
    }

    public static Result<Code> Create(string value) =>
        Result.Create(value)
            .Ensure(x => !string.IsNullOrEmpty(x),
                DomainErrors.Code.Empty)
            .Ensure(x => x.Length <= MaxLength,
                DomainErrors.Code.TooLong)
            .Ensure(x => x.Length >= MinLength,
                DomainErrors.Code.TooShort)
            .Ensure(x => x.All(char.IsLetterOrDigit),
                DomainErrors.Code.InvalidFormat)
            .Map(x => new Code(x));

    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
