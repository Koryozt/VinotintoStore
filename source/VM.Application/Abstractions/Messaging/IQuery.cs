using VM.Domain.Shared;
using MediatR;

namespace VM.Application.Abstractions.Messaging;

# pragma warning disable

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}