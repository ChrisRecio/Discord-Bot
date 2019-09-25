using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("SlowClap")]
        public async Task SlowClap()
        {
            await ReplyAsync(":clap:");
            await Task.Delay(1500);
            await ReplyAsync(":clap:");
            await Task.Delay(1500);
            await ReplyAsync(":clap:");
        }


    }
}
