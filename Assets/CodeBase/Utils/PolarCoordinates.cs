namespace PolygonGame.Utils
{
    using UnityEngine;

    public class PolarCoordinates
    {
        public static Vector2 ToCartesian(Vector2 point) =>
            ToCartesian(point.x, point.y);

        public static Vector2 ToCartesian(float radius, float angle) =>
            new Vector2(
                x: radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                y: radius * Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}