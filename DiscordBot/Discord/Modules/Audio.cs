using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.Audio;
using Discord.WebSocket;


namespace DiscordBot.Discord.Modules
{
    public class Audio : ModuleBase<SocketCommandContext>
    {

        [Command("Join")]
        [Summary("Join's Voice Channel User Is In")]
        public async Task Join()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("You need to be connected to a voice channel.");
                return;
            }
            else
            {
                IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                IAudioClient client = await channel.ConnectAsync();
            }
        }
/*

        [Command("Leave")]
        [Summary("Make''s The Bot Leave The Voice Channel")]
        public async Task Leave()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("Please join the channel the bot is in to make it leave.");
            }
            else
            {
                IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                await channel.DisconnectAsync();
            }
        }

        [Command("Play")]
        [Summary("Play's Music Requested By User")]
        public async Task Play([Remainder]string query)
            => await ReplyAsync(await _musicService.PlayAsync(query, Context.Guild.Id));

        [Command("Stop")]
        [Summary("Stop's The Bot From Playing Music")]
        public async Task Stop()
            => await ReplyAsync(await _musicService.StopAsync(Context.Guild.Id));

        [Command("Skip")]
        [Summary("Skips Current Song")]
        public async Task Skip()
            => await ReplyAsync(await _musicService.SkipAsync(Context.Guild.Id));

        [Command("Volume")]
        [Summary("Adjust Bot Volume")]
        public async Task Volume(int vol)
            => await ReplyAsync(await _musicService.SetVolumeAsync(vol, Context.Guild.Id));

        [Command("Pause")]
        [Summary("Pause's Bots Audio")]
        public async Task Pause()
            => await ReplyAsync(await _musicService.PauseOrResumeAsync(Context.Guild.Id));

        [Command("Resume")]
        [Summary("Resumes Bots Audio")]
        public async Task Resume()
            => await ReplyAsync(await _musicService.ResumeAsync(Context.Guild.Id));*/
    }
}