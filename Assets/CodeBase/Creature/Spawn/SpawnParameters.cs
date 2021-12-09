namespace PolygonGame.Creature.Spawn
{
    using System;
    using PolygonGame.Creature.Behavior;
    using UnityEngine;

    public struct SpawnParameters
    {
        public int Amount;
        public int MaxSides;
        public Rect SpawnArea;
        public RandomSelector<Func<IBehavior>> BehaviorSelector;
        public Action<ConsumeEventArgs> ConsumeHandler;
    }
}
