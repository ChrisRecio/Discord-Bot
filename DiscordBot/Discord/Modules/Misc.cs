
using Discord.Commands;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;

namespace DiscordBot.Discord.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {

        [Command("Ping")]
        [Summary("Returns Bots Ping")]
        public async Task Ping()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message For " + Context.User.Username);
            embed.WithDescription($"Ping time for bot is {Context.Client.Latency}ms");
            embed.WithColor(new Color(255, 255, 0));
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("SlowClap")]
        [Summary("Displays Clap Emoji 3 Times With Delay")]
        public async Task SlowClap()
        {
            await ReplyAsync(":clap:");
            await Task.Delay(1500);
            await ReplyAsync(":clap:");
            await Task.Delay(1500);
            await ReplyAsync(":clap:");
        }




        // External API Function Calls

        [Command("Cat Fact"), Alias("CatFact", "CatFacts", "Cat Facts")]
        [Summary("Returns Cat Facts")]
        public async Task Cat_Fact()
        {
            var json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString("https://catfact.ninja/fact");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string fact = dataObject.fact.ToString();

            var embed = new EmbedBuilder();
            embed.WithTitle("Random Cat Fact");
            embed.WithDescription(fact);
            embed.WithColor(new Color(150, 128, 200));

            await Context.Channel.SendMessageAsync("", false,embed.Build());
            
        }

    }
}
