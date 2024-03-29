namespace HalalBot;

using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.WebSocket;

public sealed class BotStatusService : DiscordClientService
{
    public BotStatusService(DiscordSocketClient client, ILogger<DiscordClientService> logger) : base(client, logger)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Client.WaitForReadyAsync(stoppingToken);
        Logger.LogInformation("Client is ready!");

        await Client.SetActivityAsync(new Game("processing nasheed..."));
    }
}