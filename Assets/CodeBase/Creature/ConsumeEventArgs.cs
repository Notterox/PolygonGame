namespace PolygonGame.Creature
{
    using UnityEngine;

    public class ConsumeEventArgs
    {
        public Creature Victim { get; set; }

        public Creature Consumer { get; set; }

        public Vector2 ContactPoint { get; set; }
    }
}
