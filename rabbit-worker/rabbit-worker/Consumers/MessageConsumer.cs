using MassTransit;
using MediatR;
using rabbit_worker.Commands;
using rabbit_worker.Contract;

namespace rabbit_worker.Consumers;

public sealed class MessageConsumer : IConsumer<Message>
{
    private readonly IMediator _mediator;

    public MessageConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<Message> context)
    {
        var command = new RabbitCommand(context.Message.Text);

        await _mediator.Send(command);
    }
}