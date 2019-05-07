using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Plugins.Views.Components;
using Game.Components;

using System.Collections.Generic;
using EcsRx.Components;

namespace Game.Blueprints
{
    public class WallBlueprint : IBlueprint
    {
        private readonly int DefaultWallHealth = 3;

        public void Apply(IEntity entity)
        {
            var components = new List<IComponent>();

            var wallComponent = new WallComponent();
            wallComponent.Health.Value = DefaultWallHealth;

            components.Add(wallComponent);
            components.Add(new ViewComponent());
            components.Add(new RandomlyPlacedComponent());

            entity.AddComponents(components.AsReadOnly());
        }
    }
}