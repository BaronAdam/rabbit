using MediatR;

namespace rabbit_web.Commands;

public sealed record MessageCommand(string Message) : IRequest;
