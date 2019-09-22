using Unity;
using Unity.Lifetime;
using Unity.Resolution;

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

        public static void RegisterTypes()
        {
            _container = new UnityContainer();
            _container.RegisterSingleton<ILogger, Logger>();
            _container.RegisterSingleton<Discord.Connection>();
        }

        public static T Resolve<T>()
        {
            
            //return (T)Container.Resolve(typeof(T),"a");
            //return (T)Container.Resolve(typeof(T), string.Empty, new PropertyOverride("a", T));
        }
    }
}