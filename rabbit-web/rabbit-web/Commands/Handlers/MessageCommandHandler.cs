using MassTransit;
using MediatR;

using rabbit_worker.Contract;

namespace rabbit_web.Commands.Handlers;

public class MessageCommandHandler : IRequestHandler<MessageCommand>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageCommandHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task<Unit> Handle(MessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message(request.Message);

        await _publishEndpoint.Publish(message, cancellationToken);
        
        return Unit.Value;
    }
}