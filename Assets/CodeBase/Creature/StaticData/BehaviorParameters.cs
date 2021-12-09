namespace PolygonGame.Creature.StaticData
{
    using System;
    using UnityEngine;

    [Serializable]
    public class BehaviorParameters
    {
        [SerializeField]
        private Color color = Color.white;
        [SerializeField]
        [Min(0f)]
        private float speed = 1.0f;

        public Color Color => color;

        public float Speed => speed;
    }
}