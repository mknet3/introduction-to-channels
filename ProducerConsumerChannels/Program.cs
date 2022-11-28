using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(Channel.CreateUnbounded<string>());
builder.Services.AddHostedService<MessageConsumer>();
var app = builder.Build();

app.MapPost("/messages", async ([FromServices]Channel<string> channel, [FromBody]string message) => 
    await channel.Writer.WriteAsync(message));

app.Run();

public class MessageConsumer : BackgroundService
{
    private readonly Channel<string> _channel;
    private readonly ILogger<MessageConsumer> _logger;

    public MessageConsumer(Channel<string> channel, ILogger<MessageConsumer> logger)
    {
        _channel = channel;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            _logger.LogInformation("Message received: {Message}", message);
        }
    }
}
