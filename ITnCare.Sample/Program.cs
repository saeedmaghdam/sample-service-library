using ITnCare.Sample;
using ITnCare.Sample.Events;
using ITnCare.Sample.RabbitMQConsumers;
using ITnCare.Service;
using ITnCare.Service.Framework;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Options;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("TestService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7281/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).UseCircuitBreakerPolicy(builder.Configuration);

builder.Services.AddITnCareService(builder.Configuration, itncareBuilder =>
{
    itncareBuilder.AddCentralizedConfiguration<ApplicationOptions>(false, true);
    itncareBuilder.AddServiceDiscovery();
    itncareBuilder.AddServiceResolver();
    itncareBuilder.AddHealthCheck();
    itncareBuilder.AddLogging();
    itncareBuilder.AddMediatR<Program>();
    itncareBuilder.AddRabbitMQ();
    itncareBuilder.AddTracing();
    itncareBuilder.AddEventOutbox(builder =>
    {
        builder.Register<OrderCreatedEvent>("OrderEvents");
        builder.Register<OrderUpdatedEvent>("OrderEvents");
        builder.Register<NewCustomerRegisteredEvent>("CustomerEvents");
    });
    itncareBuilder.AddEventInbox(builder =>
    {
        builder.Register<NewCustomerRegisteredEvent>("CustomerEvents");
    });
});

builder.Services.AddSingleton<TestConsumer>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", (ILogger<Program> logger, IOptionsMonitor<ApplicationOptions> options, IMediator mediator, IOptions<GlobalOptions> globalOptions, IRabbitMQProducer rabbitMQProducer) =>
{
    //mediator.Publish(new TestNotification { Message = "Hello Dear Service!" });
    rabbitMQProducer.Publish("TEST", "Salaaaaaaaaaaaaaaaaaam");
    return options.CurrentValue;
});

app.MapGet("/test2", async () =>
{
    //mediator.Publish(new TestNotification { Message = "Hello Dear Service!" });
    await Task.Delay(TimeSpan.FromSeconds(10));
    return Results.Problem();
});

app.MapGet("/test-tracing", async () =>
{
    using (var httpClient = new HttpClient())
    {
        await httpClient.GetAsync("https://localhost:6001/Home");
    }

    return "Ok";
});

app.MapGet("/resolve", async (IServiceResolver serviceResolver, CancellationToken cancellationToken) =>
{
    var sample = serviceResolver.ResolveServiceUriAsync("ITnCare.Sample", cancellationToken).Result;

    return sample;
});

//app.UseITnCareService(config =>
//{
//    config.AddRabbitMQConsumer(config =>
//    {
//        var testConsumer = app.Services.GetRequiredService<TestConsumer>();

//        config.Subscribe(consumerBuilder =>
//        {
//            return consumerBuilder
//                .SetQueueName("TEST")
//                .SetAutoAck(false)
//                .SetEventHandler(async (channel, model, ea) =>
//                {
//                    await testConsumer.ConsumeAsync(channel, model, ea);
//                })
//                .Build();
//        });
//    });
//});

app.UseITnCareService();

app.Run();