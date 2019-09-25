using System;
using System.IO;
using Discord;
using Discord.Commands;
using System.Net;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {

        // Chat Commands

        [Command("Clear"), Alias("Purge", "Clean")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Summary("Clears Messages")]
        public async Task Purge(int num)
        {
            var messages = await Context.Channel.GetMessagesAsync(num).FlattenAsync();
            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
        }

        [Command("Say")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Summary("Makes The Bot Say")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message From " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        // Edit The Bot

        [Command("Game"), Alias("ChangeGame", "SetGame")]
        [Remarks("Change what the bot is currently playing.")]
        [RequireOwner]
        public async Task SetGame([Remainder] string gameName)
        {
            await Context.Client.SetGameAsync(gameName);
            await ReplyAsync($"Changed game to `{gameName}`");
        }

        [Command("setAvatar"), Alias("changeAvatar")]
        [Remarks("Sets the bots Avatar")]
        [RequireOwner]
        public async Task SetAvatar(string link)
        {
            var s = Context.Message.DeleteAsync();

            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Context.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);
            }
            catch (Exception)
            {
                
                await Context.Channel.SendMessageAsync("An Error Occured While Trying To Change Profile Pictures");
            }
        }

    }
}