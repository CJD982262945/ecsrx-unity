using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using Game.Enums;
using Random = UnityEngine.Random;

using System.Collections.Generic;
using EcsRx.Components;


namespace Game.Blueprints
{
    public class EnemyBlueprint : IBlueprint
    {
        private EnemyTypes GetRandomEnemyType()
        {
            var enemyValue = Random.Range(0, 2); // Its exclusive on max, ask unity...
            return (EnemyTypes) enemyValue;
        }

        public void Apply(IEntity entity)
        {
            var components = new List<IComponent>();

            var enemyComponent = new EnemyComponent();
            enemyComponent.Health.Value = 3;
            enemyComponent.EnemyType = GetRandomEnemyType();
            enemyComponent.EnemyPower = enemyComponent.EnemyType == EnemyTypes.Regular ? 10 : 20;

            components.Add(enemyComponent);
            components.Add(new ViewComponent());
            components.Add(new MovementComponent());
            components.Add(new RandomlyPlacedComponent());

            entity.AddComponents(components.AsReadOnly());
        }
    }
}