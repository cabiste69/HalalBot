using System.Diagnostics;
using Discord;
using Discord.Interactions;
using HalalBot.Utils;

namespace HalalBot.Interactions;

public class Tests : InteractionModuleBase<SocketInteractionContext>
{
    private readonly FFmpeg _ffmpeg;
    public Tests(FFmpeg ffmpeg)
    {
        _ffmpeg = ffmpeg;
    }

    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }

    [SlashCommand("list-nasheed", "Echo an input")]
    public async Task ListNasheed()
    {
        string response = $"there's {Globals.Nasheeds.Count} nasheed: \n";
        foreach (var item in Globals.Nasheeds)
        {
            if (response.Length + item.Length >= 2000) break;
            response += item + "\n";
        }
        await RespondAsync(response);
    }

    [SlashCommand("nasheedify", "adds nasheed to a video / image")]
    public async Task AddNasheed(Attachment link)
    {
        string output = "D:\\Videos\\test.mp4";
        // await RespondAsync("");
        var message = await ReplyAsync("adding nasheed...");
        // await FollowupAsync("idk");
        if (_ffmpeg.AddNasheedToVideo(link.Url, "", output))
        {
            await RespondWithFileAsync(output);
            // await Context.Channel.SendFileAsync(output);
        }
        else
        {
            // await message.ModifyAsync(msg => msg.Content = "raped");
            // await Context.Channel.SendMessageAsync("failed :(");
            await RespondAsync("an error hapened");
        }
    }

}