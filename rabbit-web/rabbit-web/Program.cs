using FastEndpoints;
using MassTransit;
using MediatR;
using rabbit_worker.Contract;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.Send<Message>(z => z.UseRoutingKeyFormatter(_ => "test_binding"));
        cfg.Message<Message>(z => z.SetEntityName("test-exchange"));
        cfg.Publish<Message>(z =>
        {
            z.ExchangeType = ExchangeType.Direct;
        });
    });
});

var app = builder.Build();

app.UseFastEndpoints();

app.Run();