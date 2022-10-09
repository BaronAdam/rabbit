using MediatR;

namespace rabbit_worker.Commands.Handlers;

public sealed class RabbitCommandHandler : IRequestHandler<RabbitCommand>
{
    public async Task<Unit> Handle(RabbitCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        
        Console.WriteLine($"Received message: {request.Message}");
        
        return Unit.Value;
    }
}