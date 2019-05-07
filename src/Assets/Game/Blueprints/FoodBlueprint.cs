using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using UnityEngine;

using System.Collections.Generic;
using EcsRx.Components;

namespace Game.Blueprints
{
    public class FoodBlueprint : IBlueprint
    {
        private readonly int FoodValue = 10;
        private readonly int SodaValue = 20;

        private bool ShouldBeSoda()
        { return Random.Range(0, 2) == 1; }
    

        public void Apply(IEntity entity)
        {
            var components = new List<IComponent>();

            var foodComponent = new FoodComponent();
            var isSoda = ShouldBeSoda();
            foodComponent.IsSoda = isSoda;
            foodComponent.FoodAmount = isSoda ? SodaValue : FoodValue;

            components.Add(foodComponent);
            components.Add(new ViewComponent());
            components.Add(new RandomlyPlacedComponent());

            entity.AddComponents(components.AsReadOnly());
        }
    }
}