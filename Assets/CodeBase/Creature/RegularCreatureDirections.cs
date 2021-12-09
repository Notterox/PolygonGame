namespace PolygonGame.Creature
{
    using System;
    using PolygonGame.Utils;
    using UnityEngine;

    public class RegularCreatureDirections : IDirectionsProvider
    {
        private const float DefaultRotation = 90f;
        private const float FullCircle = 360f;

        private static RegularCreatureDirections instanceValue = new RegularCreatureDirections();

        public static RegularCreatureDirections Instance { get => instanceValue; }

        public Vector2 RandomDirection(int sides)
        {
            if (sides < 3)
            {
                throw new Exception("Number of sides must be creater than 3");
            }

            return GetDirection(sides, UnityEngine.Random.Range(0, sides));
        }

        private Vector2 GetDirection(int sides, int sectorIndex)
        {
            float sectorAngle = FullCircle / sides;
            float sectorCenter = (sectorAngle * sectorIndex) + DefaultRotation;

            return PolarCoordinates.ToCartesian(1f, sectorCenter);
        }
    }
}