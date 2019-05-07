using EcsRx.Infrastructure.Dependencies;
using Game.SceneCollections;
using Zenject;

namespace Game.Installers
{
    public class SceneCollectionsModule : IDependencyModule
    {
        private void SetupTiles(IDependencyContainer container)
        {
            container.Bind(new FloorTiles().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new OuterWallTiles().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new WallTiles().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new FoodTiles().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new EnemyTiles().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new ExitTiles().GetType(), new BindingConfiguration{AsSingleton = true});
        }

        private void SetupAudio(IDependencyContainer container)
        {
            container.Bind(new EnemyAttackSounds().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new PlayerAttackSounds().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new WalkingSounds().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new DeathSounds().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new DrinkSounds().GetType(), new BindingConfiguration{AsSingleton = true});
            container.Bind(new FoodSounds().GetType(), new BindingConfiguration{AsSingleton = true});
        }

        public void Setup(IDependencyContainer container)
        {
            SetupTiles(container);
            SetupAudio(container);
        }
    }
}