using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using StreamMusicBot.Services;

namespace DiscordBot.Discord.Modules
{
    public class Audio : ModuleBase<SocketCommandContext>
    {
        //private AudioService _audioService;

        //public Audio(AudioService audioService)
        //{
        //    audioService = _audioService;
        //}

        //[Command("Join")]
        //public async Task Join()
        //{
        //    var user = Context.User as SocketGuildUser;
        //    if (user.VoiceChannel is null)
        //    {
        //        await ReplyAsync("You need to connect to a voice channel.");
        //        return;
        //    }
        //    else
        //    {
        //        await _audioService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
        //    }
        //}

        //[Command("Leave")]
        //public async Task Leave()
        //{
        //    var user = Context.User as SocketGuildUser;
        //    if (user.VoiceChannel is null)
        //    {
        //        await ReplyAsync("Please join the channel the bot is in to make it leave.");
        //    }
        //    else
        //    {
        //        await _audioService.LeaveAsync(user.VoiceChannel);
        //    }
        //}

        //[Command("Play")]
        //public async Task Play([Remainder]string query)
        //    => await ReplyAsync(await _audioService.PlayAsync(query, Context.Guild.Id));

        //[Command("Stop")]
        //public async Task Stop()
        //    => await ReplyAsync(await _audioService.StopAsync(Context.Guild.Id));

        //[Command("Skip")]
        //public async Task Skip()
        //    => await ReplyAsync(await _audioService.SkipAsync(Context.Guild.Id));

        //[Command("Volume")]
        //public async Task Volume(int vol)
        //    => await ReplyAsync(await _audioService.SetVolumeAsync(vol, Context.Guild.Id));

        //[Command("Pause")]
        //public async Task Pause()
        //    => await ReplyAsync(await _audioService.PauseOrResumeAsync(Context.Guild.Id));

        //[Command("Resume")]
        //public async Task Resume()
        //    => await ReplyAsync(await _audioService.ResumeAsync(Context.Guild.Id));
    }
}