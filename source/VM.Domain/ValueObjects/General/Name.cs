using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Errors;
using VM.Domain.Primitives;
using VM.Domain.Shared;

namespace VM.Domain.ValueObjects.General;

public sealed class Name : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; private set; }

    private Name(string value)
    {
        Value = value;
    }

    private Name()
    {

    }

    public static Result<Name> Create(string name) =>
        Result.Create(name)
            .Ensure(x => !string.IsNullOrEmpty(name),
                DomainErrors.Name.Empty)
            .Ensure(x => x.Length < MaxLength,
                DomainErrors.Name.TooLong)
            .Map(x => new Name(x));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}