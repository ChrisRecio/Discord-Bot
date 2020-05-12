using System;
using Discord.Commands;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using System.Linq;
using System.Net.Http;
using Discord.WebSocket;

namespace DiscordBot.Discord.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private static readonly Color embedColor = new Color(66, 176, 255);
        private static readonly char[] _letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static readonly string[] _morseLetters = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--..", "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----.", "    ", "--..--", ".-.-.-", "----" };

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
            var embed = new EmbedBuilder();
            embed.WithTitle(Context.Client.CurrentUser.Username);
            embed.WithImageUrl(Context.Client.CurrentUser.GetAvatarUrl());
            embed.WithColor(embedColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Avatar")]
        [Summary("Returns A Users Profile Picture")]
        public async Task Avatar(SocketGuildUser user = null)
        {
            user = user ?? (SocketGuildUser)Context.User;

            var embed = new EmbedBuilder();
            embed.WithTitle(user.Username);
            embed.WithImageUrl(user.GetAvatarUrl().Replace("size=128", "size=2048"));
            embed.WithColor(embedColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Info")]
        [Summary("Returns A Users Profile Information")]
        public async Task AccountInformation(SocketGuildUser user = null)
        {
            user = user ?? (SocketGuildUser)Context.User;

            var embed = new EmbedBuilder()
                .WithAuthor($"{user.Username}'s account information", user.GetAvatarUrl())
                .AddField("Joined at: ", user.JoinedAt.Value.DateTime.ToString())
                .WithColor(embedColor)
                .WithCurrentTimestamp()
                .WithFooter($"Requested by {Context.User.Username}")
                .WithThumbnailUrl(user.GetAvatarUrl())
                .Build();

            await Context.Channel.SendMessageAsync(Context.User.Mention, false, embed);
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

        [Command("Morse")]
        [Summary("Translate Message To Morse Code")]
        public async Task Echo([Remainder] string message)
        {
            string newText = "";

            message = message.ToLower();

            foreach (var t in message)
            {
                for (short j = 0; j < 37; j++)
                {
                    if (t == _letters[j])
                    {
                        newText += _morseLetters[j];
                        newText += "   ";
                        break;
                    }
                }
            }
            await ReplyAsync(newText);
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
            embed.WithColor(embedColor);

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
            embed.WithColor(embedColor);

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
            embed.WithColor(embedColor);

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
            embed.WithColor(embedColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        #endregion

    }
}
