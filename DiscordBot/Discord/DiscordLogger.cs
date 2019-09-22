using System.Threading.Tasks;
using Discord;

namespace DiscordBot.Discord
{
    public class DiscordLogger
    {
        ILogger _logger;
        public DiscordLogger(ILogger logger)
        {
            _logger = logger;
        }


        public Task Log(LogMessage logMsg)
        {
            _logger.Log(logMsg.Message);
            return  Task.CompletedTask;
        }
        
    }
}