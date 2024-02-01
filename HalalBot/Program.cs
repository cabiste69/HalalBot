using HalalBot;
using HalalBot.Utils;

using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

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
        GatewayIntents = GatewayIntents.All
    };

    config.Token = builder.Configuration["Token"]!;
});

builder.Services.AddCommandService((config, _) =>
{
    config.DefaultRunMode = RunMode.Async;
    config.CaseSensitiveCommands = false;
});

builder.Services.AddInteractionService((config, _) =>
{
    config.LogLevel = LogSeverity.Verbose;
    config.UseCompiledLambda = true;
});

// Add any other services here
builder.Services.AddHostedService<InteractionHandler>();
builder.Services.AddHostedService<CommandHandler>();
builder.Services.AddHostedService<BotStatusService>();
builder.Services.AddTransient<FFmpeg>();
// builder.Services.AddHostedService<LongRunningService>();

Globals.Init(builder.Configuration["NasheedPath"]!);

var host = builder.Build();
await host.RunAsync();
