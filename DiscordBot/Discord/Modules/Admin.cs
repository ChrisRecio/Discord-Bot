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
        // Admin Embed Color RGB(116, 255, 56)

        // Chat Commands
        #region Chat Commands

        [Command("Clear"), Alias("Purge", "Clean")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Summary("Clears Messages")]
        public async Task Purge(int num)
        {
            var messages = await Context.Channel.GetMessagesAsync(num).FlattenAsync();
            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
            var m = await ReplyAsync($"Cleared `{num}` Messages");
            await Task.Delay(5000);
            await m.DeleteAsync();
        }

        [Command("Say")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Summary("Makes The Bot Say")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message From " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(116, 255, 56));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        #endregion


        // Edit The Bot
        #region Edit The Bot

        [Command("Game"), Alias("ChangeGame", "SetGame")]
        [Summary("Change what the bot is currently playing.")]
        [RequireOwner]
        public async Task SetGame([Remainder] string gameName)
        {
            await Context.Client.SetGameAsync(gameName);
            await ReplyAsync($"Changed game to `{gameName}`");
        }

        [Command("Nick")]
        [Summary("Changes the Bots Nickname on the current guild.")]
        [RequireOwner]
        public async Task Nick(string n)
        {
            var user = Context.Guild.GetUser(512340575039651840);
            await user.ModifyAsync(x => { x.Nickname = n; });
            var m = await ReplyAsync($"Changed Nickname To `{n}`");
            await Task.Delay(5000);
            await m.DeleteAsync();
        }

        [Command("setAvatar"), Alias("changeAvatar")]
        [Summary("Sets the bots Avatar")]
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

        #endregion

    }
}