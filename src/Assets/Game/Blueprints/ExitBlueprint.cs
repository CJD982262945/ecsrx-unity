using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Plugins.Views.Components;
using Game.Components;

using System.Collections.Generic;
using EcsRx.Components;

namespace Game.Blueprints
{
    public class ExitBlueprint : IBlueprint
    {
        public void Apply(IEntity entity)
        {
            var components = new List<IComponent>();
            components.Add(new ExitComponent());
            components.Add(new ViewComponent());
            components.Add(new RandomlyPlacedComponent());

            entity.AddComponents(components.AsReadOnly());
        }
    }
}