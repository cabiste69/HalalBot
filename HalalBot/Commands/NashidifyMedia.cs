namespace HalalBot.Commands;

using Discord.Commands;

public class PublicModule : ModuleBase<SocketCommandContext>
{
    
    [Command("ping")]
    [Alias("pong", "hello")]
    public async Task PingAsync()
    {
        await ReplyAsync("pong!");
    }

}