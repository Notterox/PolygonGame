namespace PolygonGame.Creature.StaticData
{
    using System;
    using UnityEngine;

    [Serializable]
    public class CreatureParameters
    {
        [SerializeField]
        private Material material;
        [SerializeField]
        [Min(0f)]
        private float size = 1.0f;
        [SerializeField]
        private BehaviorParameters passiveBehavior;
        [SerializeField]
        private BehaviorParameters straightBehavior;
        [SerializeField]
        private RandomBehaviorParameters randomBehavior;
        [SerializeField]
        private AggressiveBehaviorParameters aggressiveBehavior;

        public Material Material => material;

        public float Size => size;

        public BehaviorParameters PassiveBehavior => passiveBehavior;

        public BehaviorParameters StraightBehavior => straightBehavior;

        public RandomBehaviorParameters RandomBehavior => randomBehavior;

        public AggressiveBehaviorParameters AggressiveBehavior => aggressiveBehavior;
    }
}