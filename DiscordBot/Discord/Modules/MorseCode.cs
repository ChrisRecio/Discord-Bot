using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Discord.Modules
{
    public class MorseCode : ModuleBase<SocketCommandContext>
    {
        readonly char[] _letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        readonly string[] _morseLetters = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--..", "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----.", "    ", "--..--", ".-.-.-", "----" };

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
    }
}