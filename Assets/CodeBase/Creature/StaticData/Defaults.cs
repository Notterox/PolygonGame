namespace PolygonGame.Creature.StaticData
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Defaults", menuName = "GameData/Defaults", order = 1)]
    public class Defaults : ScriptableObject
    {
        private static Defaults instance;

        [SerializeField]
        private CreatureParameters creature;
        [SerializeField]
        private LayerMask borderMask;

        public CreatureParameters Creature => creature;

        public LayerMask BorderMask => borderMask;

        public static Defaults GetInstance()
        {
            if (instance == null)
            {
            }

            return instance;
        }

        public static bool LoadDefaults()
        {
            instance = Resources.Load<Defaults>("StaticData/Defaults");

            return instance != null;
        }
    }
}