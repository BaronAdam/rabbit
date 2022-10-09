using MediatR;

namespace rabbit_worker.Commands;

public sealed record RabbitCommand(string Message) : IRequest;