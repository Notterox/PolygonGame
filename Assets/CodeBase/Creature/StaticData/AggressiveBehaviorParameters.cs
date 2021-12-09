namespace PolygonGame.Creature.StaticData
{
    using System;
    using UnityEngine;

    [Serializable]
    public class AggressiveBehaviorParameters : BehaviorParameters
    {
        [SerializeField]
        private float changeTargetTime = 3f;

        public float ChangeTargetTime => changeTargetTime;
    }
}
