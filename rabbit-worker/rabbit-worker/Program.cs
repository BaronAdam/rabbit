using System.Reflection;
using MassTransit;
using MediatR;
using rabbit_worker;
using rabbit_worker.Consumers;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetEntryAssembly());

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("test-queue", q =>
                {
                    q.ConfigureConsumeTopology = false;
                    q.Durable = true;
                    q.AutoDelete = false;
                    q.DeadLetterExchange = "test-exchange-dl";
                    q.PrefetchCount = 2;

                    q.ConfigureConsumer<MessageConsumer>(ctx);

                    q.Bind("test-exchange", e =>
                    {
                        e.Durable = true;
                        e.AutoDelete = false;
                        e.ExchangeType = ExchangeType.Direct;
                        e.RoutingKey = "test_binding";
                    });

                    // q.BindDeadLetterQueue("test-exchange-dl", "test-queue-dl", dl =>
                    // {
                    //     dl.Durable = true;
                    //     dl.AutoDelete = false;
                    //     dl.ExchangeType = ExchangeType.Fanout;
                    // });
                });
            });
        });
        services.AddMediatR(typeof(Program));
    })
    .Build();

await host.RunAsync();