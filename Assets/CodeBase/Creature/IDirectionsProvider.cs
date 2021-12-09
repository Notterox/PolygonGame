namespace PolygonGame.Creature
{
    using UnityEngine;

    public interface IDirectionsProvider
    {
        Vector2 RandomDirection(int sides);
    }
}
