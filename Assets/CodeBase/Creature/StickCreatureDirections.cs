namespace PolygonGame.Creature
{
    using UnityEngine;

    public class StickCreatureDirections : IDirectionsProvider
    {
        private static StickCreatureDirections instanceValue = new StickCreatureDirections();

        public static StickCreatureDirections Instance { get => instanceValue; }

        public Vector2 RandomDirection(int sides) =>
            Random.value > 0.5
                ? Vector2.up
                : Vector2.down;
    }
}