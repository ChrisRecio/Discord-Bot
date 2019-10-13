using Discord.WebSocket;
using DiscordBot.Discord;
using DiscordBot.Discord.Entities;
using DiscordBot.Storage;
using DiscordBot.Storage.Implementations;
using Unity;
using Unity.Injection;
using Unity.Resolution;
using Victoria;

namespace DiscordBot
{
    public static class Unity
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            get
            {
                if (_container == null)
                    RegisterTypes();
                return _container;
            }
        }

        public static object Interception { get; internal set; }

        public static void RegisterTypes()
        {
            _container = new UnityContainer();

            // Bot Connection
            _container.RegisterType<DiscordSocketClient>(new InjectionFactory(i => SocketConfig.GetDefault()));
            _container.RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)));
            _container.RegisterSingleton<Discord.Connection>();
            _container.RegisterSingleton<CommandHandler>();

            // Storage
            _container.RegisterSingleton<IDataStorage, JsonStorage>();
            _container.RegisterSingleton<ILogger, Logger>();

            // Music
            _container.RegisterSingleton<LavaSocketClient>();
        }

        public static T Resolve<T>()
        {
            return (T)Container.Resolve(typeof(T), string.Empty, new CompositeResolverOverride());
        }

    }
}