using HalalBot;
using Discord;
using Discord.WebSocket;
using Discord.Addons.Hosting;
using HalalBot.Utils;

// CreateApplicationBuilder configures a lot of stuff for us automatically
// See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host
var builder = Host.CreateApplicationBuilder(args);

// Configure Discord.NET
builder.Services.AddDiscordHost((config, _) =>
{
    config.SocketConfig = new DiscordSocketConfig
    {
        LogLevel = LogSeverity.Verbose,
        AlwaysDownloadUsers = true,
        MessageCacheSize = 200,
        GatewayIntents = GatewayIntents.All,
        UseInteractionSnowflakeDate = false
    };

    config.Token = builder.Configuration["Token"]!;
});

// Optionally wire up the interaction service
builder.Services.AddInteractionService((config, _) =>
{
    config.LogLevel = LogSeverity.Verbose;
    config.UseCompiledLambda = true;
});

// Add any other services here
builder.Services.AddHostedService<InteractionHandler>();
builder.Services.AddHostedService<BotStatusService>();
builder.Services.AddTransient<FFmpeg>();
// builder.Services.AddHostedService<LongRunningService>();

var host = builder.Build();
Globals.Init(builder.Configuration["NasheedPath"]!);
await host.RunAsync();
