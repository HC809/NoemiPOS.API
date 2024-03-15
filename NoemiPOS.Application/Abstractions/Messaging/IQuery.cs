using MediatR;
using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
