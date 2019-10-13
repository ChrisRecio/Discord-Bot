
using System;
using Discord.Commands;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using System.Linq;
using System.Net.Http;

namespace DiscordBot.Discord.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        // User Embed Color RGB(66, 176, 255)


        // Chat Commands
        #region Chat Commands

        [Command("Ping")]
        [Summary("Returns Bots Ping")]
        public async Task Ping()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message For " + Context.User.Username);
            embed.WithDescription($"Ping time for bot is {Context.Client.Latency}ms");
            embed.WithColor(new Color(66, 176, 255));
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("SlowClap"), Alias("Clap")]
        [Summary("Displays Clap Emoji 3 Times With Delay")]
        public async Task SlowClap()
        {
            await ReplyAsync(":clap:");
            await Task.Delay(1500);
            await ReplyAsync(":clap:");
            await Task.Delay(1500);
            await ReplyAsync(":clap:");
        }

        [Command("Walter")]
        [Summary("Walter")]
        public async Task Walter()
        {
            await ReplyAsync("Walter");
        }

        [Command("Avatar")]
        [Summary("Returns A Users Profile Picture")]
        public async Task Avatar()
        {
            var user = Context.Message.MentionedUsers.FirstOrDefault();
            var url = user?.GetAvatarUrl();
            await ReplyAsync($"{user?.Username}'s Avatar is: {url?.Replace("size=128", "size=1024")}");
        }

        [Command("Ascii")]
        [Summary("Returns your Text as ASCII Art")]
        public async Task Ascii([Remainder] string text)
        {
            string result;
            var handler = new HttpClientHandler();
            using (var httpClient = new HttpClient(handler, false))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get,
                    "http://artii.herokuapp.com/make?text=" + text.Replace(" ", "+")))
                {
                    var response = await httpClient.SendAsync(request);
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            try
            {
                await ReplyAsync("```" + result + "```");
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("8ball"), Alias("EightBall")]
        [Summary("Answers all your questions in life.")]
        [RequireBotPermission(ChannelPermission.EmbedLinks)]
        public async Task EightBall([Remainder] string question)
        {
            string result;
            var handler = new HttpClientHandler();
            using (var httpClient = new HttpClient(handler, false))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get,
                    "https://8ball.delegator.com/magic/JSON/" + question))
                {
                    var response = await httpClient.SendAsync(request);
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            result = result.Substring(result.IndexOf("er\": \"", StringComparison.Ordinal) + 6);
            result = result.Remove(result.IndexOf("\",", StringComparison.Ordinal));
            var embed = new EmbedBuilder()
                .WithColor(66, 176, 255)
                .WithAuthor(author =>
                {
                    author
                        .WithName("Magic 8 Ball")
                        .WithIconUrl("https://raw.githubusercontent.com/brh55/spark-magic-8-ball/master/bot-icon.png");
                })
                .WithDescription(result);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("Mock"), Alias("Sarcastic")]
        [Summary("rEpEaTs yOuR tExT lIkE tHiS.")]
        public async Task Mock([Remainder] string input = "")
        {
            string result = "";
            if (input == "")
            {
                var message = await Context.Channel.GetMessagesAsync(2).FlattenAsync();
                input = message.Last().Content;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                {
                    result += char.ToLower(input[i]);
                }
                else
                {
                    result += char.ToUpper(input[i]);
                }
            }
            await ReplyAsync(result);
        }

        [Command("Os")]
        [Summary("Displays What Os The Bot Is Running On")]
        public async Task Os()
        {
            await ReplyAsync($"I'm currently running on {Environment.OSVersion}.");
        }

        #endregion

        // External API Calls
        #region External API Calls

        [Command("Cat Fact"), Alias("CatFact", "CatFacts", "Cat Facts")]
        [Summary("Returns Cat Facts")]
        public async Task CatFact()
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
            embed.WithColor(new Color(66, 176, 255));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("RandomCat"), Alias("Random Cat")]
        [Summary("Returns An Image Of A Cat")]
        public async Task RandomCat()
        {
            var json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString("http://aws.random.cat/meow");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string datImage = dataObject.file.ToString();

            var embed = new EmbedBuilder();
            embed.WithTitle("Random Cat");
            embed.WithImageUrl(datImage);
            embed.WithColor(new Color(66, 176, 255));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("RandomDog"), Alias("Random Dog")]
        [Summary("Returns An Image Of A Dog")]
        public async Task RandomDog()
        {
            var json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString("https://dog.ceo/api/breeds/image/random");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string dogImage = dataObject.message.ToString();

            var embed = new EmbedBuilder();
            embed.WithTitle("Random Dog");
            embed.WithImageUrl(dogImage);
            embed.WithColor(new Color(66, 176, 255));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("RandomDoge"), Alias("Random Doge", "Doge", "Adam")]
        [Summary("Returns An Image Of A Doge")]
        public async Task RandomDoge()
        {
            var json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString("http://shibe.online/api/shibes?count=1");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string dogeImage = dataObject.ToString();

            dogeImage = dogeImage.Remove(0, 6);
            dogeImage = dogeImage.Remove(dogeImage.Length - 4, 4);

            var embed = new EmbedBuilder();
            embed.WithTitle("Random Doge");
            embed.WithImageUrl(dogeImage);
            embed.WithColor(new Color(66, 176, 255));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        #endregion

    }
}
