using FastEndpoints;
using MediatR;
using rabbit_web.Commands;
using rabbit_web.Models;

namespace rabbit_web.Endpoints;

public class PostMessageEndpoint : Endpoint<MessageRequest, EmptyResponse>
{
    private readonly IMediator _mediator;

    public PostMessageEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("message");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MessageRequest req, CancellationToken ct)
    {
        var command = new MessageCommand(req.Message);

        await _mediator.Send(command, ct);
    }
}