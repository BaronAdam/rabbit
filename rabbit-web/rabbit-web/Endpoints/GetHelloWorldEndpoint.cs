using FastEndpoints;
using rabbit_web.Models;

namespace rabbit_web.Endpoints;

public class GetHelloWorldEndpoint : EndpointWithoutRequest<HelloWorldResponse>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new HelloWorldResponse
        {
            Message = "Hello world!"
        };

        await SendAsync(response, cancellation: ct);
    }
}