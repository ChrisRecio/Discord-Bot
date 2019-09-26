using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Modules
{
    public class Math : ModuleBase<SocketCommandContext>
    {
        [Command("Addition"), Alias("Add")]
        [Summary("Add 2 Numbers.")]
        public async Task AddAsync(float num1, float num2)
        {
            await ReplyAsync($"The Answer To That Is: {num1 + num2}");
        }

        [Command("Subtract")]
        [Summary("Subtract 2 numbers.")]
        public async Task SubtractAsync(float num1, float num2)
        {
            await ReplyAsync($"The Answer To That Is: {num1 - num2}");
        }

        [Command("Multiply")]
        [Summary("Multiply 2 Numbers.")]
        public async Task MultiplyAsync(float num1, float num2)
        {
            await ReplyAsync($"The Answer To That Is {num1 * num2}");
        }

        [Command("Divide")]
        [Summary("Divide 2 Numbers.")]
        public async Task DivideAsync(float num1, float num2)
        {
            await ReplyAsync($"The Answer To That Is: {num1 / num2}");
        }
    }
}