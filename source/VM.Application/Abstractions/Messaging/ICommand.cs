using VM.Domain.Shared;
using MediatR;

namespace VM.Application.Abstractions.Messaging;

# pragma warning disable

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
