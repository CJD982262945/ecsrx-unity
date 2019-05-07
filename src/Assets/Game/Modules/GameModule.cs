using EcsRx.Infrastructure.Dependencies;
using Game.Configuration;

namespace Game.Installers
{
    public class GameModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind(new GameConfiguration().GetType(), new BindingConfiguration{AsSingleton = true});
        }
    }
}