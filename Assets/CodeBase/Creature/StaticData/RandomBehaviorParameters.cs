namespace PolygonGame.Creature.StaticData
{
    using System;
    using UnityEngine;

    [Serializable]
    public class RandomBehaviorParameters : BehaviorParameters
    {
        [SerializeField]
        private float changeDirectionTime = 3f;

        public float ChangeDirectionTime => changeDirectionTime;
    }
}
