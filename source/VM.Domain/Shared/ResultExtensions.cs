using System;

namespace VM.Domain.Shared;

public static class ResultExtensions
{
    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Error error)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (result.IsFailure)
        {
            return result;
        }

        return predicate(result.Value) ?
            result :
            Result.Failure<T>(error);
    }

    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mappingFunc)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (mappingFunc is null)
        {
            throw new ArgumentNullException(nameof(mappingFunc));
        }

        return result.IsSuccess ?
            Result.Success(mappingFunc(result.Value)) :
            Result.Failure<TOut>(result.Error);
    }
}
